using System;
using UnityEngine;

public class Lander : MonoBehaviour
{
    public static Lander Instance { get; private set; }
    [SerializeField] private float speed = 700f;
    [SerializeField] private float turnSpeed;
    private Rigidbody2D rg;
    private State state;
    private float fuel = 0f;
    private float maxFuel = 10f;
    public event EventHandler OnUpForce;
    public event EventHandler OnLeftForce;
    public event EventHandler OnRightForce;
    public event EventHandler OnBeforeForce;
    public event EventHandler OnCoinPickup;
    public event EventHandler OnFuelPickup;
    public event EventHandler<OnLandedEventArgs> OnLanded;
    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
    private const float GRAVITY_NORMAL = 0.7F;
    public class OnStateChangedEventArgs : EventArgs
    {
        public State state;
    }
    public class OnLandedEventArgs : EventArgs
    {
        public LandingResult landingResult;
        public int score;
        public float dotVector;
        public float landingSpeed;
        public float scoreMultiplier;
    }
    private void Awake()
    {
        Instance = this;
        rg = GetComponent<Rigidbody2D>();
        rg.gravityScale = 0f;
        fuel = maxFuel;
    }
    public enum State
    {
        WaitingToStart,
        Normal,
        GameOver,
    }
    public enum LandingResult
    {
        Success,
        WrongAreaLanding,
        TooSteepAngle,
        TooFastLanding,
    }
    private void FixedUpdate()
    {
        OnBeforeForce?.Invoke(this, EventArgs.Empty);
        switch (state)
        {
            case State.WaitingToStart:
                if (GameInput.Instance.UpActionPressed() ||
                    GameInput.Instance.LeftActionPressed() ||
                    GameInput.Instance.RightActionPressed())
                {
                    SetState(State.Normal);
                    rg.gravityScale = GRAVITY_NORMAL;
                    ConsumeFuel();
                }
                break;
            case State.Normal:
                if (fuel <= 0) return;
                if (GameInput.Instance.UpActionPressed() ||
                    GameInput.Instance.LeftActionPressed() ||
                    GameInput.Instance.RightActionPressed())
                {
                    ConsumeFuel();
                }
                if (GameInput.Instance.UpActionPressed())
                {
                    rg.AddForce(transform.up * speed * Time.deltaTime);
                    OnUpForce?.Invoke(this, EventArgs.Empty);
                }
                if (GameInput.Instance.LeftActionPressed())
                {
                    rg.AddTorque(turnSpeed * Time.deltaTime);
                    OnLeftForce?.Invoke(this, EventArgs.Empty);
                }
                if (GameInput.Instance.RightActionPressed())
                {
                    rg.AddTorque(-turnSpeed * Time.deltaTime);
                    OnRightForce?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GameOver:
                break;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision2D)
    {
        if (collision2D.gameObject.TryGetComponent(out Coin coin))
        {

            OnCoinPickup?.Invoke(this, EventArgs.Empty);
            coin.OnDestroy();
        }
        if (collision2D.gameObject.TryGetComponent(out Fuel fuelReload))
        {
            float fuelAmount = 10f;
            fuel += fuelAmount;
            if (fuel > maxFuel) fuel = maxFuel;
            OnFuelPickup?.Invoke(this, EventArgs.Empty);
            fuelReload.OnDestroy();
        }

    }
    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (!collision2D.gameObject.TryGetComponent(out LandingPad landingPad))
        {
            OnLanded?.Invoke(this, new OnLandedEventArgs
            {
                landingResult = LandingResult.WrongAreaLanding,
                score = 0,
                dotVector = 0,
                landingSpeed = 0,
                scoreMultiplier = 0,
            });
            Debug.Log("Wrong Area");
            SetState(State.GameOver);
            return;
        }
        float landingSpeed = collision2D.relativeVelocity.magnitude;
        float maxLandingVelocity = 4f;
        if (landingSpeed > maxLandingVelocity)
        {
            OnLanded?.Invoke(this, new OnLandedEventArgs
            {
                landingResult = LandingResult.TooFastLanding,
                score = 0,
                dotVector = 0,
                landingSpeed = landingSpeed,
                scoreMultiplier = 0,
            });
            Debug.Log("Too Fast Speed");
            SetState(State.GameOver);
            return;
        }
        float dotVector = Vector2.Dot(transform.up, Vector2.up);
        float minVector = 0.9f;
        if (dotVector < minVector)
        {
            OnLanded?.Invoke(this, new OnLandedEventArgs
            {
                landingResult = LandingResult.TooSteepAngle,
                score = 0,
                dotVector = dotVector,
                landingSpeed = 0,
                scoreMultiplier = 0,
            });
            Debug.Log("Too Steep Angle");
            SetState(State.GameOver);
            return;
        }
        float maxLandingAngleScoreBonus = 50;
        float landingAngleScoreBonus = dotVector * maxLandingAngleScoreBonus;
        Debug.Log("A " + landingAngleScoreBonus);
        float scoreLandingSpeedScoreBonus = 50;
        float landingSpeedScoreBonus = (1 - landingSpeed / maxLandingVelocity) * scoreLandingSpeedScoreBonus;
        Debug.Log("B " + landingSpeedScoreBonus);
        int score = Mathf.RoundToInt(landingAngleScoreBonus + landingSpeedScoreBonus) * landingPad.GetScoreMultiplier();
        Debug.Log("Được cộng tổng " + score);
        OnLanded?.Invoke(this, new OnLandedEventArgs
        {
            landingResult = LandingResult.Success,
            score = score,
            dotVector = dotVector,
            landingSpeed = landingSpeed,
            scoreMultiplier = landingPad.GetScoreMultiplier(),
        });
        Debug.Log("Thành công");
        SetState(State.GameOver);
        return;
    }
    private void ConsumeFuel()
    {
        float amount = 1f;
        fuel -= amount * Time.deltaTime;
    }
    private void SetState(State state)
    {
        this.state = state;
        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs { state = state, });
    }
    public float GetSpeedX()
    {
        return rg.linearVelocity.x;
    }
    public float GetSpeedY()
    {
        return rg.linearVelocity.y;
    }
    public float GetFuelNormalized()
    {
        return fuel / maxFuel;
    }
}

