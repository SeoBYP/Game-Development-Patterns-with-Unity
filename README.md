# 유니티 게임 디자인 패턴
---

## Adapter Pattern (어댑터 패턴)

### 개요
어댑터 패턴은 호환되지 않는 두 개의 인터페이스를 조정하여 기존 코드를 수정하지 않고 다른 인터페이스와 연결하는 패턴입니다. 특히 리팩터링할 수 없는 레거시 코드나 수정이 어려운 서드파티 라이브러리와 같은 경우에 유용합니다.

### 어댑터 패턴의 접근 방식
- **객체 어댑터 (Object Adapter)**: 객체 구성을 사용하여 어댑터가 조정할 객체를 래핑합니다. 
- **클래스 어댑터 (Class Adapter)**: 상속을 통해 어댑터가 기존 클래스의 인터페이스를 다른 클래스의 인터페이스에 적용합니다.

어댑터 패턴에서는 어댑터 클래스가 클라이언트와 어댑티(Adaptee) 사이에 위치하며, 클라이언트는 어댑터를 통해 인터페이스와 통신합니다. 

### 주의할 점
퍼사드 패턴과 어댑터 패턴은 종종 헷갈리기 쉬운데, 퍼사드는 복잡한 시스템에 대한 단순화된 전면 인터페이스를 제공하는 반면, 어댑터는 호환되지 않는 인터페이스 사이를 조정합니다.

### 코드 예시

#### IInventorySystem Interface
`IInventorySystem` 인터페이스는 어댑터를 통해 통신할 일관된 메서드 시그니처를 정의합니다.

```csharp
public interface IInventorySystem
{
    void SyncInventories();
    void AddItem(InventoryItem item, SaveLocation location);
    void RemoveItem(InventoryItem item, SaveLocation location);
    List<InventoryItem> GetInventory(SaveLocation location);
}
```

#### InventorySystem Class
`InventorySystem`은 원래의 인벤토리 시스템을 관리하는 클래스로, 클라우드에 아이템을 추가하거나 삭제하는 기본 기능을 포함합니다.

```csharp
public class InventorySystem 
{
    public void AddItem(InventoryItem item) { Debug.Log("Adding item to the cloud"); }
    public void RemoveItem(InventoryItem item) { Debug.Log("Removing item from the cloud"); }
    public List<InventoryItem> GetInventory() { return new List<InventoryItem>(); }
}
```

#### InventorySystemAdapter Class
`InventorySystemAdapter`는 `InventorySystem`을 `IInventorySystem` 인터페이스와 호환되도록 조정하는 어댑터 클래스입니다. `InventorySystem`의 기능을 확장하여 로컬 및 클라우드에 아이템을 추가하거나 삭제할 수 있습니다.

```csharp
public class InventorySystemAdapter : InventorySystem, IInventorySystem
{
    public void SyncInventories() { Debug.Log("Synchronizing local drive and cloud inventories"); }

    public void AddItem(InventoryItem item, SaveLocation location)
    {
        if (location == SaveLocation.Cloud) AddItem(item);
        if (location == SaveLocation.Local) Debug.Log("Adding item to local drive");
        if (location == SaveLocation.Both) Debug.Log("Adding item to local drive and on the cloud");
    }

    public void RemoveItem(InventoryItem item, SaveLocation location) { Debug.Log("Remove item from local/cloud/both"); }
    public List<InventoryItem> GetInventory(SaveLocation location) { return new List<InventoryItem>(); }
}
```

#### ClientAdapter Class
`ClientAdapter` 클래스는 UI 버튼을 통해 어댑터를 사용해 인벤토리에 아이템을 추가하거나 삭제하는 기능을 제공합니다.

```csharp
public class ClientAdapter : MonoBehaviour
{
    public InventoryItem item;
    private InventorySystem _inventorySystem;
    private IInventorySystem _inventorySystemAdapter;

    private void Start()
    {
        _inventorySystem = new InventorySystem();
        _inventorySystemAdapter = new InventorySystemAdapter();
    }

    private void OnGUI()
    {
        if(GUILayout.Button("Add item(no Adapter)")) { _inventorySystem.AddItem(item); }
        if (GUILayout.Button("Add item (with Adapter)")) { _inventorySystemAdapter.AddItem(item, SaveLocation.Both); }
    }
}
```

### 장단점

#### 장점
- **수정 없이 조정 가능**: 오래된 코드나 서드파티 코드를 수정하지 않고 쉽게 적용할 수 있습니다.
- **재사용성 및 유연성**: 레거시 코드를 새로운 시스템에서 재사용할 수 있어 개발 비용이 절감됩니다.

#### 단점
- **지속적인 레거시 사용**: 레거시 코드 사용은 비용 효율적이지만, 새로운 시스템에서 호환되지 않을 위험이 있습니다.
- **약간의 성능 저하**: 호출을 리다이렉션하여 약간의 성능 저하가 발생할 수 있습니다.

### 사용 예시
어댑터 패턴은 유니티의 서드파티 라이브러리에서 핵심 클래스를 수정하지 않고 필요한 기능을 추가할 때 유용합니다.

---

## Command Pattern (커맨드 패턴)

### 개요
커맨드 패턴은 '액션'을 수행하거나 상태 변경을 트리거하는 데 필요한 정보를 캡슐화하여, 액션 요청을 특정 객체와 분리하는 디자인 패턴입니다. 이 패턴은 명령을 큐에 넣거나 나중에 실행할 수 있도록 하여 되돌리기, 재실행, 매크로, 자동화와 같은 기능을 쉽게 구현할 수 있게 합니다.

### 커맨드 패턴의 구성 요소
- **Invoker (호출자)**: 명령을 실행하고 기록하는 객체입니다.
- **Receiver (수신자)**: 명령을 받아서 실제로 수행하는 객체입니다.
- **Command**: 특정 작업을 실행하는 메서드를 정의하는 추상 클래스 또는 인터페이스입니다.
- **Concrete Command**: `Command`를 구현하여 구체적인 작업을 수행하는 클래스입니다.

이 예시에서는 `BikeController`가 Receiver 역할을 수행하며, `Command`와 이를 상속받은 `TurnLeft`, `TurnRight`, `ToggleTurbo` 클래스가 실제 명령을 구현합니다. `Invoker` 클래스는 명령을 기록하고 재생하는 기능을 담당합니다.

### 코드 예시

#### Command 추상 클래스
`Command`는 모든 명령이 상속받아야 하는 추상 클래스로, `Execute()` 메서드를 정의합니다.

```csharp
public abstract class Command 
{
    public abstract void Execute();
}
```

#### Concrete Command 클래스들
각각의 `Concrete Command`는 `Command`를 상속받아 특정 작업을 수행하는 `Execute()` 메서드를 구현합니다.

##### ToggleTurbo
`ToggleTurbo`는 `BikeController`의 터보 모드를 토글하는 명령입니다.

```csharp
public class ToggleTurbo : Command
{
    private BikeController _controller;

    public ToggleTurbo(BikeController controller) { _controller = controller; }

    public override void Execute() { _controller.ToggleTurbo(); }
}
```

##### TurnLeft
`TurnLeft`는 `BikeController`를 왼쪽으로 이동시키는 명령입니다.

```csharp
public class TurnLeft : Command
{
    private BikeController _controller;

    public TurnLeft(BikeController controller) { _controller = controller; }

    public override void Execute() { _controller.Turn(BikeController.Direction.Left); }
}
```

##### TurnRight
`TurnRight`는 `BikeController`를 오른쪽으로 이동시키는 명령입니다.

```csharp
public class TurnRight : Command
{
    private BikeController _controller;

    public TurnRight(BikeController controller) { _controller = controller; }

    public override void Execute() { _controller.Turn(BikeController.Direction.Right); }
}
```

#### Invoker 클래스
`Invoker`는 명령을 기록하고, 기록된 명령을 재생하는 역할을 합니다.

```csharp
public class Invoker : MonoBehaviour
{
    private bool _isRecording;
    private bool _isReplaying;
    private float _replayTime;
    private float _recordingTime;
    private SortedList<float, Command> _recordedCommands = new SortedList<float, Command>();

    public void ExecuteCommand(Command command)
    {
        command.Execute();
        if (_isRecording) _recordedCommands.Add(_recordingTime, command);
    }

    public void Record() { _recordingTime = 0.0f; _isRecording = true; }
    public void Replay() { _replayTime = 0.0f; _isReplaying = true; }
}
```

#### BikeController 클래스
`BikeController`는 수신자로, 명령이 실행될 때 실제 작업을 수행하는 클래스입니다.

```csharp
public class BikeController : MonoBehaviour
{
    public enum Direction { Left = -1, Right = 1 }

    private bool _isTurboOn;
    private float _distance = 1.0f;

    public void ToggleTurbo()
    {
        _isTurboOn = !_isTurboOn;
        Debug.Log("Turbo Active : " + _isTurboOn);
    }

    public void Turn(Direction direction)
    {
        if (direction == Direction.Left) transform.Translate(Vector3.left * _distance);
        if (direction == Direction.Right) transform.Translate(Vector3.right * _distance);
    }

    public void ResetPosition() { transform.position = Vector3.zero; }
}
```

#### InputHandler 클래스
`InputHandler`는 입력에 따라 명령을 실행하거나 기록하는 클래스입니다.

```csharp
public class InputHandler : MonoBehaviour
{
    private Invoker _invoker;
    private BikeController _bikeController;
    private Command _buttonA, _buttonD, _buttonW;

    void Start()
    {
        _invoker = gameObject.AddComponent<Invoker>();
        _bikeController = FindObjectOfType<BikeController>();
        _buttonA = new TurnLeft(_bikeController);
        _buttonD = new TurnRight(_bikeController);
        _buttonW = new ToggleTurbo(_bikeController);
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.A)) _invoker.ExecuteCommand(_buttonA);
        if (Input.GetKeyUp(KeyCode.D)) _invoker.ExecuteCommand(_buttonD);
        if (Input.GetKeyUp(KeyCode.W)) _invoker.ExecuteCommand(_buttonW);
    }
}
```

### 장단점

#### 장점
- **분리**: 실행 방법을 알고 있는 객체에서 작업을 호출하는 객체를 분리할 수 있습니다.
- **시퀀싱**: 되돌리기/재실행 기능, 매크로, 명령 큐 구현을 쉽게 할 수 있습니다.

#### 단점
- **복잡성**: 각 명령이 클래스 형태로 구현되므로 코드가 복잡해질 수 있습니다.

### 사용 예시
커맨드 패턴은 실행 취소/재실행, 매크로 기능, 자동화 시스템 구현에 유용합니다.

---

## Decorator Pattern (데코레이터 패턴)
### 개요
데코레이터 패턴은 객체의 구조를 변경하지 않고도 런타임에 객체의 기능을 동적으로 추가하는 디자인 패턴입니다. 이 패턴은 객체를 '래핑'하여 새로운 기능을 추가하며, 상속 대신 조합을 통해 확장성을 제공합니다.

### 데코레이터 패턴의 구성 요소
- **Component (구성 요소)**: 데코레이팅될 객체의 인터페이스 또는 추상 클래스.
- **Concrete Component (구체 구성 요소)**: 기본 동작을 구현하는 객체.
- **Decorator (데코레이터)**: `Component`를 구현하여 새로운 기능을 추가하는 역할.
- **Concrete Decorator (구체 데코레이터)**: `Decorator`의 구체적 구현으로, 실제로 기능을 확장함.

이 예시에서는 `IWeapon` 인터페이스가 Component 역할을 하며, `Weapon` 클래스가 기본 동작을 수행하는 `Concrete Component`로, `WeaponDecorator`가 데코레이터 역할을 수행합니다.

### 코드 예시

#### IWeapon Interface
`IWeapon` 인터페이스는 무기와 데코레이터 간 일관된 메서드 시그니처를 정의합니다.

```csharp
public interface IWeapon
{
    float Range { get; }
    float Rate { get; }
    float Strength { get; }
    float Cooldown { get; }
}
```

#### Weapon Class
`Weapon` 클래스는 `IWeapon` 인터페이스를 구현하고, 무기의 기본 속성을 정의합니다.

```csharp
public class Weapon : IWeapon
{
    private readonly WeaponConfig _config;

    public Weapon(WeaponConfig config) { _config = config; }

    public float Range => _config.Range;
    public float Rate => _config.Rate;
    public float Strength => _config.Strength;
    public float Cooldown => _config.Cooldown;
}
```

#### WeaponDecorator Class
`WeaponDecorator` 클래스는 무기를 래핑하여 속성을 향상시키는 역할을 합니다.

```csharp
public class WeaponDecorator : IWeapon
{
    private readonly IWeapon _decoratedWeapon;
    private readonly WeaponAttachment _attachment;

    public WeaponDecorator(IWeapon decoratedWeapon, WeaponAttachment attachment)
    {
        _decoratedWeapon = decoratedWeapon;
        _attachment = attachment;
    }

    public float Range => _decoratedWeapon.Range + _attachment.Range;
    public float Rate => _decoratedWeapon.Rate + _attachment.Rate;
    public float Strength => _decoratedWeapon.Strength + _attachment.Strength;
    public float Cooldown => _decoratedWeapon.Cooldown + _attachment.Cooldown;
}
```

#### WeaponAttachment Class
`WeaponAttachment`는 무기에 추가할 수 있는 부착물의 속성을 정의합니다.

```csharp
[CreateAssetMenu(fileName = "NewWeaponAttachment", menuName = "Weapon/Attachment", order = 1)]
public class WeaponAttachment : ScriptableObject, IWeapon
{
    public float rate;
    public float range;
    public float strength;
    public float cooldown;

    public float Rate => rate;
    public float Range => range;
    public float Strength => strength;
    public float Cooldown => cooldown;
}
```

#### BikeWeapon Class
`BikeWeapon` 클래스는 무기 초기화와 데코레이터 패턴을 통해 무기 부착물을 추가하거나 제거하는 기능을 담당합니다.

```csharp
public class BikeWeapon : MonoBehaviour
{
    public WeaponConfig weaponConfig;
    public WeaponAttachment mainAttachment;
    public WeaponAttachment secondaryAttachment;

    private IWeapon _weapon;
    private bool _isDecorated;

    private void Start() { _weapon = new Weapon(weaponConfig); }

    public void Decorate()
    {
        if (mainAttachment && !secondaryAttachment)
            _weapon = new WeaponDecorator(_weapon, mainAttachment);
        else if (mainAttachment && secondaryAttachment)
            _weapon = new WeaponDecorator(new WeaponDecorator(_weapon, mainAttachment), secondaryAttachment);
        
        _isDecorated = true;
    }

    public void Reset()
    {
        _weapon = new Weapon(weaponConfig);
        _isDecorated = false;
    }
}
```

#### ClientDecorator Class
`ClientDecorator`는 UI 버튼을 통해 무기에 부착물을 추가하거나 제거하고 무기를 발사할 수 있습니다.

```csharp
public class ClientDecorator : MonoBehaviour
{
    private BikeWeapon _bikeWeapon;

    void Start() { _bikeWeapon = FindObjectOfType<BikeWeapon>(); }

    private void OnGUI()
    {
        if (GUILayout.Button("Decorate Weapon")) { _bikeWeapon.Decorate(); }
        if (GUILayout.Button("Reset Weapon")) { _bikeWeapon.Reset(); }
    }
}
```

### 장단점

#### 장점
- **유연한 객체 확장**: 런타임에 객체의 기능을 추가하거나 제거할 수 있습니다.
- **서브클래싱의 대안**: 조합을 사용해 상속보다 유연하게 기능을 추가합니다.

#### 단점
- **복잡한 계층 구조**: 여러 데코레이터가 중첩되면 관계가 복잡해질 수 있습니다.
- **관리의 어려움**: 데코레이터가 많아지면 유지보수와 코드 관리가 어려울 수 있습니다.

### 사용 예시
데코레이터 패턴은 게임의 무기 시스템에서 다양한 부착물을 추가하거나 제거하여 무기 기능을 확장하는 데 유용합니다.

---

## Event Bus Pattern (이벤트 버스 패턴)

### 개요
이벤트 버스 패턴은 시스템 내 여러 컴포넌트 간의 메시지 또는 이벤트를 전달하는 중앙 허브 역할을 합니다. 이벤트 버스를 통해 클래스 간 의존성을 최소화하면서 이벤트 기반 시스템을 구축할 수 있습니다.

### 주요 구성 요소
- **EventListener**: 특정 이벤트 유형을 수신하고, 이벤트 발생 시 호출되는 메서드를 정의하는 인터페이스입니다.
- **GameEventManager**: 이벤트를 구독하고, 이벤트를 트리거하여 모든 구독자에게 이벤트를 전달하는 중앙 관리자입니다.
- **GameStatesEvent**: 특정 게임 상태를 나타내는 이벤트 구조체로, 게임 이벤트 타입을 포함합니다.

이 예제에서는 `GameEventManager`가 이벤트를 구독, 해제, 트리거하는 역할을 하고, 여러 컴포넌트가 이벤트를 수신하여 특정 동작을 수행합니다.

### 코드 예시

#### EventListener 인터페이스
`EventListener`는 이벤트를 수신하는 기본 인터페이스로, `OnEvent()` 메서드를 통해 이벤트를 처리합니다.

```csharp
public interface EventListener<T> : EventListenerBase
{
    void OnEvent(T eventType);
}
```

#### GameEventManager 클래스
`GameEventManager`는 이벤트를 관리하며, 구독, 해제, 이벤트 트리거 기능을 제공합니다.

```csharp
public class GameEventManager
{
    private static readonly Dictionary<Type, List<EventListenerBase>> _subscribersList = new Dictionary<Type, List<EventListenerBase>>();

    public static void Subscribe<GameEvents>(EventListener<GameEvents> listener) where GameEvents : struct
    {
        Type eventType = typeof(GameEvents);
        if (!_subscribersList.ContainsKey(eventType)) _subscribersList[eventType] = new List<EventListenerBase>();
        if (!SubscriptionExists(eventType, listener)) _subscribersList[eventType].Add(listener);
    }

    public static void Unsubscribe<GameEvents>(EventListener<GameEvents> listener) where GameEvents : struct
    {
        Type eventType = typeof(GameEvents);
        if (!_subscribersList.ContainsKey(eventType)) return;
        List<EventListenerBase> subscriberList = _subscribersList[eventType];
        for (int i = subscriberList.Count - 1; i >= 0; i--)
        {
            if (subscriberList[i] == listener)
            {
                subscriberList.Remove(subscriberList[i]);
                if (subscriberList.Count == 0) _subscribersList.Remove(eventType);
                return;
            }
        }
    }

    public static void TriggerEvent<GameEvents>(GameEvents events) where GameEvents : struct
    {
        Type eventType = typeof(GameEvents);
        if (!_subscribersList.TryGetValue(eventType, out var list)) return;
        foreach (EventListener<GameEvents> listener in list) listener.OnEvent(events);
    }
}
```

#### GameStatesEvent 구조체
`GameStatesEvent`는 특정 게임 상태를 나타내는 이벤트 구조체로, `GameEventType`을 포함합니다.

```csharp
public struct GameStatesEvent
{
    public GameEventType gameEventType;

    public GameStatesEvent(GameEventType gameEventType) { this.gameEventType = gameEventType; }
}
```

#### HUDController 클래스
`HUDController`는 `GameStatesEvent`를 구독하고, `START` 이벤트 발생 시 HUD를 표시합니다.

```csharp
public class HUDController : MonoBehaviour, EventListener<GameStatesEvent>
{
    private bool _isDisplayOn;

    private void OnEnable() { this.EventStartingListening<GameStatesEvent>(); }
    private void OnDisable() { this.EventStopListening<GameStatesEvent>(); }

    public void OnEvent(GameStatesEvent eventType)
    {
        if (eventType.gameEventType == GameEventType.START) _isDisplayOn = true;
    }

    private void OnGUI()
    {
        if (_isDisplayOn && GUILayout.Button("Stop Game"))
            GameEventManager.TriggerEvent(new GameStatesEvent { gameEventType = GameEventType.STOP });
    }
}
```

#### CountDownTimer 클래스
`CountDownTimer`는 `COUNTDOWN` 이벤트를 수신하면 카운트다운을 시작하고, 완료 시 `START` 이벤트를 트리거합니다.

```csharp
public class CountDownTimer : MonoBehaviour, EventListener<GameStatesEvent>
{
    private float _duration = 3.0f;

    public void OnEvent(GameStatesEvent eventType)
    {
        if (eventType.gameEventType == GameEventType.COUNTDOWN) StartCoroutine(CountDown());
    }

    private IEnumerator CountDown()
    {
        float currentTime = _duration;
        while (currentTime > 0)
        {
            yield return new WaitForSeconds(1.0f);
            currentTime--;
        }
        GameEventManager.TriggerEvent(new GameStatesEvent { gameEventType = GameEventType.START });
    }
}
```

### 장단점

#### 장점
- **느슨한 결합**: 클래스 간의 의존성을 줄여 재사용성과 유지보수성을 높입니다.
- **유연한 확장성**: 새로운 이벤트 리스너를 쉽게 추가할 수 있습니다.

#### 단점
- **디버깅의 어려움**: 이벤트가 발생하고 전달되는 과정을 추적하기 어렵습니다.
- **메모리 누수 가능성**: 이벤트 구독 해제를 누락할 경우 메모리 누수가 발생할 수 있습니다.

### 사용 예시
이벤트 버스 패턴은 게임 상태 전환, UI 업데이트, 플레이어와 NPC 간 상호작용 등 다양한 이벤트 기반 시스템에 적합합니다.

---


## Facade Pattern (퍼사드 패턴)

### 개요
퍼사드 패턴은 시스템의 복잡한 내부 작업을 추상화하여 단순화된 전면 인터페이스를 제공하는 패턴입니다. 
게임 개발에 유용한 패턴으로, 다양한 시스템 간 상호작용을 단순화하여 외부에서 쉽게 사용할 수 있게 합니다.

퍼사드 패턴의 이름은 건물의 'Facade(전면)'에서 유래하였으며, 복잡한 내부 구조를 감추고 단순한 인터페이스를 통해 상호작용하게 합니다.

### 장단점

#### 장점
- **복잡한 시스템에 단순화된 인터페이스 제공**: 퍼사드 클래스를 통해 복잡한 시스템과의 상호작용을 쉽게 할 수 있습니다.
- **리팩터링 용이**: 퍼사드 뒤에 숨겨진 코드를 수정해도 외부 인터페이스는 변하지 않아 클라이언트 코드에 영향을 주지 않고 리팩터링할 수 있습니다.

#### 단점
- **지저분한 코드의 은폐 가능성**: 퍼사드를 통해 복잡한 코드를 감추기만 한다면, 장기적으로 유지보수하기 어려운 코드가 될 수 있습니다.
- **퍼사드 남용의 위험**: 전역 접근이 가능한 매니저 클래스를 남용하면, 각 클래스 간의 결합도가 높아져 유지보수가 어려운 코드가 될 수 있습니다.

### 코드 예시

#### BikeEngine 클래스
`BikeEngine` 클래스는 엔진의 다양한 서브 시스템을 초기화하고 제어합니다. `TurnOn()`, `TurnOff()`, `ToggleTurbo()` 메서드를 통해 외부에서 쉽게 엔진을 조작할 수 있습니다.

```csharp
public class BikeEngine : MonoBehaviour
{
    private FuelPump _fuelPump;
    private TurboCharger _turboCharger;
    private CoolingSystem _coolingSystem;

    private void Awake()
    {
        _fuelPump = gameObject.AddComponent<FuelPump>();
        _turboCharger = gameObject.AddComponent<TurboCharger>();
        _coolingSystem = gameObject.AddComponent<CoolingSystem>();
    }

    public void TurnOn()
    {
        StartCoroutine(_fuelPump.burnFuel);
        StartCoroutine(_coolingSystem.coolEngine);
    }

    public void TurnOff()
    {
        StopCoroutine(_fuelPump.burnFuel);
        StopCoroutine(_coolingSystem.coolEngine);
    }

    public void ToggleTurbo() { _turboCharger.ToggleTurbo(_coolingSystem); }
}
```

#### FuelPump 클래스
`FuelPump` 클래스는 연료를 태우는 작업을 담당합니다. 연료가 다 떨어지면 엔진을 종료합니다.

```csharp
public class FuelPump : MonoBehaviour
{
    public BikeEngine engine;
    public IEnumerator burnFuel;

    void Start() { burnFuel = BurnFuel(); }

    IEnumerator BurnFuel()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            engine.fuelAmount -= engine.burnRate;
            if (engine.fuelAmount <= 0.0f) { engine.TurnOff(); yield return 0; }
        }
    }
}
```

#### CoolingSystem 클래스
`CoolingSystem` 클래스는 엔진 온도를 관리하며, `PauseCooling()`으로 냉각 작업을 중지하거나 재개할 수 있습니다.

```csharp
public class CoolingSystem : MonoBehaviour
{
    public BikeEngine engine;
    public IEnumerator coolEngine;

    private void Start() { coolEngine = CoolEngine(); }

    IEnumerator CoolEngine()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (engine.currentTemp > engine.minTemp) engine.currentTemp -= engine.tempRate;
            if (engine.currentTemp < engine.minTemp) engine.currentTemp += engine.tempRate;
            if (engine.currentTemp > engine.maxTemp) engine.TurnOff();
        }
    }
}
```

#### TurboCharger 클래스
`TurboCharger` 클래스는 엔진에 터보를 추가하여 일정 시간 동안 출력을 증가시킵니다.

```csharp
public class TurboCharger : MonoBehaviour 
{
    public BikeEngine engine;
    private CoolingSystem _coolingSystem;
    public bool _isTurboOn;

    public void ToggleTurbo(CoolingSystem coolingSystem)
    {
        _coolingSystem = coolingSystem;
        if (!_isTurboOn) StartCoroutine(TurboCharge());
    }

    IEnumerator TurboCharge()
    {
        _isTurboOn = true;
        _coolingSystem.PauseCooling();

        yield return new WaitForSeconds(engine.turboDuration);

        _isTurboOn = false;
        _coolingSystem.PauseCooling();
    }
}
```

#### ClientFacade 클래스
`ClientFacade` 클래스는 퍼사드 인터페이스를 통해 간단하게 엔진을 제어하는 역할을 합니다.

```csharp
public class ClientFacade : MonoBehaviour
{
    private BikeEngine _bikeEngine;

    private void Start() { _bikeEngine = gameObject.AddComponent<BikeEngine>(); }

    private void OnGUI()
    {
        if (GUILayout.Button("Turn On")) _bikeEngine.TurnOn();
        if (GUILayout.Button("Turn Off")) _bikeEngine.TurnOff();
        if (GUILayout.Button("Toggle Turbo")) _bikeEngine.ToggleTurbo();
    }
}
```

### 사용 예시
퍼사드 패턴은 복잡한 서브 시스템을 단순화된 인터페이스로 통합해야 할 때 유용합니다. 예를 들어, 게임에서 복잡한 엔진 동작을 외부에서 쉽게 제어할 수 있도록 퍼사드 클래스를 사용하여 엔진 시스템의 세부 사항을 감출 수 있습니다.

---


## State Pattern (Finite State Machine, FSM) - 상태 패턴

### 개요
상태 패턴(FSM)은 객체가 특정 상태에 따라 다른 동작을 하도록 하는 디자인 패턴입니다. 객체의 상태 전환을 관리하고, 상태별 행동을 정의하여 복잡한 상태 전환 로직을 쉽게 처리할 수 있습니다.

### FSM 구성 요소
- **Context**: 현재 상태를 포함하며, 상태 전환을 관리하는 클래스입니다.
- **State Interface**: 개별 상태가 구현해야 하는 공통 인터페이스입니다.
- **Concrete States**: 상태별로 구체적인 동작을 정의하는 클래스입니다.

이 예제에서는 `BikeController`가 `Context` 역할을 수행하고, `BikeStateContext`는 현재 상태를 관리하며 `IBikeState` 인터페이스를 구현한 구체적인 상태(`BikeStartState`, `BikeStopState`, `BikeTurnState`)로 상태 전환을 처리합니다.

### 코드 예시

#### IBikeState 인터페이스
`IBikeState`는 모든 상태가 구현해야 하는 `Handle()` 메서드를 정의합니다.

```csharp
public interface IBikeState
{
    void Handle(BikeController controller);
}
```

#### BikeController 클래스 (Context)
`BikeController`는 상태를 전환하며, 현재 상태에 따라 자전거의 행동을 결정합니다.

```csharp
public class BikeController : MonoBehaviour
{
    private IBikeState _startState, _stopState, _turnState;
    private BikeStateContext _bikeStateContext;

    void Start()
    {
        _bikeStateContext = new BikeStateContext(this);
        _startState = gameObject.AddComponent<BikeStartState>();
        _stopState = gameObject.AddComponent<BikeStopState>();
        _turnState = gameObject.AddComponent<BikeTurnState>();
        _bikeStateContext.Transition(_stopState);
    }

    public void StartBike() { _bikeStateContext.Transition(_startState); }
    public void StopBike() { _bikeStateContext.Transition(_stopState); }
    public void Turn(Direction direction) { _bikeStateContext.Transition(_turnState); }
}
```

#### BikeStateContext 클래스
`BikeStateContext`는 현재 상태를 추적하며, 상태 전환을 수행합니다.

```csharp
public class BikeStateContext 
{
    public IBikeState CurrentState { get; set; }
    private readonly BikeController _controller;

    public BikeStateContext(BikeController controller) { _controller = controller; }

    public void Transition(IBikeState state)
    {
        CurrentState = state;
        CurrentState.Handle(_controller);
    }
}
```

#### BikeStartState 클래스
`BikeStartState`는 자전거가 시작 상태일 때의 동작을 정의합니다.

```csharp
public class BikeStartState : MonoBehaviour, IBikeState
{
    private BikeController _bikeController;

    public void Handle(BikeController controller)
    {
        _bikeController = controller;
        _bikeController.CurrentSpeed = _bikeController._maxSpeed;
    }

    void Update()
    {
        if (_bikeController.CurrentSpeed > 0)
            _bikeController.transform.Translate(Vector3.forward * (_bikeController.CurrentSpeed * Time.deltaTime));
    }
}
```

#### BikeStopState 클래스
`BikeStopState`는 자전거가 멈춘 상태일 때의 동작을 정의합니다.

```csharp
public class BikeStopState : MonoBehaviour, IBikeState
{
    private BikeController _bikeController;

    public void Handle(BikeController controller)
    {
        _bikeController = controller;
        _bikeController.CurrentSpeed = 0;
    }
}
```

#### BikeTurnState 클래스
`BikeTurnState`는 자전거가 좌우로 회전하는 상태일 때의 동작을 정의합니다.

```csharp
public class BikeTurnState : MonoBehaviour, IBikeState
{
    private Vector3 _turnDirection;
    private BikeController _bikeController;

    public void Handle(BikeController controller)
    {
        _bikeController = controller;
        _turnDirection.x = (float)_bikeController.CurrentTurnDirection;
        if (_bikeController.CurrentSpeed > 0)
            transform.Translate(_turnDirection * _bikeController._turnDistance);
    }
}
```

#### ClientState 클래스
`ClientState`는 버튼을 통해 `BikeController`의 상태를 전환할 수 있는 간단한 UI를 제공합니다.

```csharp
public class ClientState : MonoBehaviour
{
    private BikeController _bikeController;

    void Start()
    {
        _bikeController = (BikeController)FindObjectOfType(typeof(BikeController));
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Start Bike")) _bikeController.StartBike();
        if (GUILayout.Button("Turn Left")) _bikeController.Turn(Direction.Left);
        if (GUILayout.Button("Turn Right")) _bikeController.Turn(Direction.Right);
        if (GUILayout.Button("Stop Bike")) _bikeController.StopBike();
    }
}
```

### 장단점

#### 장점
- **명확한 상태 관리**: 상태별로 코드가 분리되어 유지보수와 확장성이 향상됩니다.
- **유연성**: 새로운 상태를 쉽게 추가하고, 상태 전환을 관리할 수 있습니다.

#### 단점
- **클래스 증가**: 상태가 많아질수록 클래스 수도 늘어나 코드가 복잡해질 수 있습니다.

### 사용 예시
상태 패턴은 상태에 따라 다른 행동을 하는 게임 캐릭터, AI, 애니메이션 등 다양한 시스템에서 유용하게 활용할 수 있습니다.

---

## Observer Pattern (옵저버 패턴)

### 개요
옵저버 패턴은 주체(Subject) 객체의 상태 변화가 있을 때 관련된 관찰자(Observer) 객체에 자동으로 알림을 보내는 디자인 패턴입니다. 
주체는 여러 관찰자를 등록할 수 있으며, 주체의 상태가 변경되면 각 관찰자에게 이를 알리는 일대다 관계를 설정합니다.

### 옵저버 패턴의 주요 메서드
- **AttachObserver**: 주체에 관찰자를 등록하는 메서드입니다.
- **DetachObserver**: 주체에서 관찰자를 제거하는 메서드입니다.
- **NotifyObservers**: 주체가 관찰자들에게 상태 변경을 알리기 위한 메서드입니다.

### 장단점

#### 장점
- **역동성**: 필요에 따라 관찰자를 추가하거나 제거할 수 있으며, 상태 변경에 대해 다수의 객체에 알릴 수 있습니다.
- **일대다 관계**: 이벤트 기반 시스템에서 여러 객체에 알림을 보내는 문제를 해결합니다.

#### 단점
- **무질서**: 기본 옵저버 패턴에서는 관찰자들에게 알림이 전달되는 순서가 보장되지 않습니다.
- **메모리 누수 위험**: 주체가 관찰자에 대한 강한 참조를 가지며, 해제되지 않을 경우 메모리 누수가 발생할 수 있습니다.

### 코드 예시

#### Subject 클래스
`Subject`는 관찰자를 관리하며 상태가 변경될 때마다 관찰자들에게 알림을 보냅니다.

```csharp
public abstract class Subject : MonoBehaviour
{
    private readonly ArrayList _observers = new ArrayList();

    public void Attach(Observer observer) { _observers.Add(observer); }
    public void Detach(Observer observer) { _observers.Remove(observer); }

    public void NotifyObservers()
    {
        foreach (Observer observer in _observers) { observer.Notify(this); }
    }
}
```

#### Observer 클래스
`Observer`는 추상 클래스이며, 주체의 상태 변경 시 호출되는 `Notify()` 메서드를 정의합니다.

```csharp
public abstract class Observer : MonoBehaviour
{
    public abstract void Notify(Subject subject);
}
```

#### BikeController 클래스 (주체)
`BikeController`는 `Subject`를 상속받아 상태 변경을 관리하고, 상태가 변경되면 `NotifyObservers()`를 호출하여 알립니다.

```csharp
public class BikeController : Subject
{
    private float _health = 100.0f;

    public void TakeDamage(float amount)
    {
        _health -= amount;
        NotifyObservers();
        if (_health <= 0) Destroy(gameObject);
    }
}
```

#### CameraController 클래스 (관찰자)
`CameraController`는 `Observer`를 상속받아, `Notify()` 메서드에서 `BikeController`의 상태를 반영합니다.

```csharp
public class CameraController : Observer
{
    private bool _isTurboOn;

    public override void Notify(Subject subject)
    {
        var bikeController = subject.GetComponent<BikeController>();
        if (bikeController) _isTurboOn = bikeController.IsTurboOn;
    }
}
```

#### HUDController 클래스 (관찰자)
`HUDController`는 자전거의 상태(체력, 터보 상태 등)를 HUD에 반영합니다.

```csharp
public class HUDController : Observer
{
    private float _currentHealth;
    private bool _isTurboOn;

    public override void Notify(Subject subject)
    {
        var bikeController = subject.GetComponent<BikeController>();
        if (bikeController)
        {
            _isTurboOn = bikeController.IsTurboOn;
            _currentHealth = bikeController.CurrentHealth;
        }
    }
}
```

#### ClientObserver 클래스
`ClientObserver`는 사용자 입력을 통해 `BikeController`의 상태를 변경할 수 있는 간단한 UI를 제공합니다.

```csharp
public class ClientObserver : MonoBehaviour
{
    private BikeController _bikeController;

    private void Start()
    {
        _bikeController = (BikeController)FindObjectOfType<BikeController>();
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Damage Bike")) _bikeController.TakeDamage(15.0f);
        if (GUILayout.Button("Toggle Turbo")) _bikeController.ToggleTurbo();
    }
}
```

### 사용 예시
옵저버 패턴은 주체의 상태 변화에 따라 여러 객체에 알림을 보내야 하는 시스템에 적합합니다. 예를 들어, 게임 캐릭터의 체력 변화가 있을 때 UI, 카메라 효과, 오디오 등이 이에 반응하도록 구현할 수 있습니다.

---

## Service Locator Pattern (서비스 로케이터 패턴)

### 개요
서비스 로케이터 패턴은 특정 서비스의 종속성을 관리하고, 이들 서비스를 중앙 레지스트리에서 조회하여 제공합니다. 클라이언트는 
직접적인 참조 없이 필요한 서비스를 호출할 수 있습니다. 이 패턴은 종속성 주입과 유사한 역할을 하며, 특히 전역적으로 접근할 
필요가 있는 서비스에 적합합니다.

### 장단점

#### 장점
- **런타임 최적화**: 런타임 시 동적으로 최적화된 라이브러리나 컴포넌트를 감지하여 애플리케이션 성능을 향상시킬 수 있습니다.
- **단순성**: 종속성 관리가 간단하며, 학습이 용이하여 빠르게 프로젝트에 적용할 수 있습니다.

#### 단점
- **블랙박스화**: 레지스트리에서 종속성을 숨겨서 코드 이해가 어려워질 수 있습니다.
- **전역적 종속성**: 남용하면 관리가 어려운 전역 종속성을 초래할 수 있습니다.

### 코드 예시

#### ServiceLocator 클래스
`ServiceLocator` 클래스는 서비스의 등록 및 조회를 담당합니다.

```csharp
public static class ServiceLocator 
{
    public static readonly IDictionary<Type, object> Services = new Dictionary<Type, object>();

    public static void RegisterService<T>(T service)
    {
        if (!Services.ContainsKey(typeof(T)))
        {
            Services[typeof(T)] = service;
        }
        else
        {
            throw new ApplicationException("Service already registered");
        }
    }

    public static T GetService<T>()
    {
        try { return (T)Services[typeof(T)]; }
        catch { throw new ApplicationException("Requested service not found"); }
    }
}
```

#### 서비스 인터페이스 및 구현
서비스 계약은 인터페이스로 정의되며, 구체적인 구현 클래스는 해당 인터페이스를 구현합니다.

##### IAdvertisement 인터페이스 및 Advertisement 클래스
`IAdvertisement`는 광고를 표시하는 계약을 정의하며, `Advertisement`는 이를 구현한 클래스입니다.

```csharp
public interface IAdvertisement { void DisplayAd(); }

public class Advertisement : IAdvertisement
{
    public void DisplayAd() { Debug.Log("Displaying video advertisement"); }
}
```

##### IAnalyticsService 인터페이스 및 Analytics 클래스
`IAnalyticsService`는 분석 이벤트 전송을 정의하며, `Analytics`는 이를 구현합니다.

```csharp
public interface IAnalyticsService { void SendEvent(string eventName); }

public class Analytics : IAnalyticsService
{
    public void SendEvent(string eventName) { Debug.Log("Sent: " + eventName); }
}
```

##### ILoggerService 인터페이스 및 Logger 클래스
`ILoggerService`는 로그 메시지를 기록하는 계약을 정의하며, `Logger`는 이를 구현합니다.

```csharp
public interface ILoggerService { void Log(string message); }

public class Logger : ILoggerService
{
    public void Log(string message) { Debug.Log("Logged: " + message); }
}
```

#### ClientServiceLocator 클래스
`ClientServiceLocator`는 서비스 로케이터 패턴을 사용하여 서비스를 호출하는 예시입니다.

```csharp
public class ClientServiceLocator : MonoBehaviour
{
    private void Start() { RegisterServices(); }

    private void RegisterServices()
    {
        ILoggerService logger = new Logger();
        ServiceLocator.RegisterService(logger);

        IAnalyticsService analytics = new Analytics();
        ServiceLocator.RegisterService(analytics);

        IAdvertisement advertisement = new Advertisement();
        ServiceLocator.RegisterService(advertisement);
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Log Event"))
        {
            ILoggerService logger = ServiceLocator.GetService<ILoggerService>();
            logger.Log("Hello World!");
        }
        if (GUILayout.Button("Send Analytics"))
        {
            IAnalyticsService analytics = ServiceLocator.GetService<IAnalyticsService>();
            analytics.SendEvent("Hello World!");
        }
        if (GUILayout.Button("Display Advertisement"))
        {
            IAdvertisement advertisement = ServiceLocator.GetService<IAdvertisement>();
            advertisement.DisplayAd();
        }
    }
}
```

### 사용 예시
서비스 로케이터 패턴은 클라이언트가 특정 서비스의 구현체를 몰라도 필요한 서비스를 얻을 수 있도록 할 때 유용합니다. 예를 들어 광고, 로그, 분석 등의 전역 서비스에 대한 접근을 제공할 수 있습니다.

---

## Singleton Pattern (싱글톤 패턴)

### 개요
싱글톤 패턴은 클래스의 인스턴스가 단 하나만 존재하도록 보장하는 디자인 패턴입니다. 이 패턴은 전역 접근이 필요한 객체에 주로 사용되며, 
게임 관리 클래스, 설정 클래스 등에서 자주 사용됩니다.

### 장단점

#### 장점
- **유일성 보장**: 인스턴스가 하나만 존재하도록 보장하여 메모리 사용을 최적화하고, 같은 데이터를 공유할 수 있습니다.
- **전역 접근**: 인스턴스에 전역적으로 접근할 수 있어 필요한 곳에서 쉽게 사용할 수 있습니다.

#### 단점
- **의존성 증가**: 전역 인스턴스를 남용할 경우, 코드 간의 의존성이 증가하여 유지보수가 어려워질 수 있습니다.
- **멀티스레드 문제**: 동기화 처리가 부족할 경우 멀티스레드 환경에서 문제가 발생할 수 있습니다.

### 코드 예시

#### Singleton 클래스
`SingleTon<T>`는 제네릭 싱글톤 클래스이며, 해당 클래스를 상속받는 모든 클래스는 단 하나의 인스턴스를 가지게 됩니다.

```csharp
public class SingleTon<T> : MonoBehaviour where T : MonoBehaviour
{
    protected static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<T>();
                if (_instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(T).Name + "_AutoCreated";
                    _instance = obj.AddComponent<T>();
                }
            }
            return _instance;
        }
    }

    protected virtual void Awake()
    {
        InitializeSingleton();
    }

    protected virtual void InitializeSingleton()
    {
        _instance = this as T;
    }
}
```

#### SingletonExample 클래스
`SingletonExample` 클래스는 `SingleTon<T>` 클래스를 상속받아 싱글톤 인스턴스를 관리하며, 게임 점수 관리 기능을 포함하고 있습니다.

```csharp
public class SingletonExample : SingleTon<SingletonExample>
{
    private static int _score;

    public static int Score { get { return _score; } private set { _score = value; } }

    public void IncreaseScore(int points) { Score += points; }
}
```

#### GameController 클래스
`GameController`는 `SingletonExample` 인스턴스를 사용하여 게임 점수를 증가시키고 표시합니다.

```csharp
public class GameController : MonoBehaviour
{
    void Start()
    {
        SingletonExample.Instance.IncreaseScore(10);
        Debug.Log("현재 점수: " + SingletonExample.Score);
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(10, 10, 200, 50), "점수: " + SingletonExample.Score);
        if (GUI.Button(new Rect(10, 70, 200, 50), "점수 5 점 증가"))
        {
            SingletonExample.Instance.IncreaseScore(5);
        }
    }
}
```

#### GameManager 클래스
`GameManager` 클래스는 게임 세션 관리에 싱글톤 패턴을 사용하여, 게임 시작 및 종료 시간 등을 기록합니다.

```csharp
public class GameManager : SingleTon<GameManager>
{
    private DateTime _sessionStartTime;

    private void Start()
    {
        _sessionStartTime = DateTime.Now;
        Debug.Log("게임 세션 시작 시간 @: " + DateTime.Now);
    }

    private void OnApplicationQuit()
    {
        DateTime _sessionEndTime = DateTime.Now;
        TimeSpan timeDifference = _sessionEndTime.Subtract(_sessionStartTime);
        Debug.Log("게임 세션 종료 시간 @: " + DateTime.Now);
        Debug.Log("게임 세션 지속 시간: " + timeDifference);
    }
}
```

### 사용 예시
싱글톤 패턴은 전역으로 접근해야 하는 게임 관리, 데이터 저장소, 설정 관리 등의 클래스에서 자주 사용됩니다. 이 패턴을 통해 하나의 인스턴스를 중앙에서 관리하고 접근할 수 있어 편리합니다.

---

## Spatial Partition Pattern (공간 분할 패턴)

### 개요
공간 분할 패턴은 가상 공간을 작은 구역으로 나누어 각 구역에 객체를 배치하고, 이를 통해 빠르게 특정 객체를 검색하거나 조작할 수 있도록 하는 패턴입니다. 이 패턴은 주로 레이싱 게임, 물리 시뮬레이션, 그리고 복잡한 3D 환경에서 객체를 효율적으로 관리하는 데 사용됩니다.

이번 예제에서는 레이싱 트랙을 일정한 세그먼트로 나누어 관리하는 방법을 보여줍니다. 플레이어의 위치와 세그먼트의 거리에 따라 세그먼트를 로드하거나 삭제하여 메모리와 성능을 최적화합니다.

### 장단점

#### 장점
- **성능 최적화**: 특정 구역에 필요한 객체만 로드하고 나머지 구역의 객체는 언로드하여 메모리와 CPU 사용을 줄입니다.
- **효율적인 검색**: 객체를 공간 구역에 따라 분할하여 필요한 객체를 빠르게 찾을 수 있습니다.

#### 단점
- **구현 복잡성**: 객체를 특정 구역에 따라 동적으로 로드 및 언로드해야 하므로 구현이 다소 복잡할 수 있습니다.
- **비용 증가**: 추가적인 데이터 구조가 필요할 수 있으며, 이를 위한 초기화 및 관리 비용이 증가할 수 있습니다.

### 코드 예시

#### TrackController 클래스
`TrackController`는 트랙의 세그먼트를 관리하고 로드 및 언로드를 담당합니다.

```csharp
public class TrackController : MonoBehaviour
{
    private List<GameObject> _segments;
    private Stack<GameObject> _segStack;
    private Transform _segParent;
    private Vector3 _currentPosition = new Vector3(0, 0, 0);

    [SerializeField] private Track track;
    [SerializeField] private int initSegAmount;
    [SerializeField] private int incrSegAmount;

    private void Awake()
    {
        _segments = Enumerable.Reverse(track.segments).ToList();
    }

    private void Start() { InitTrack(); }

    private void InitTrack()
    {
        _segStack = new Stack<GameObject>(_segments);
        LoadSegment(initSegAmount);
    }

    private void LoadSegment(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            if (_segStack.Count > 0)
            {
                GameObject segment = Instantiate(_segStack.Pop(), _segParent.transform);
                _currentPosition.z = (_segStack.Count > 0) ? _currentPosition.z + track.segmentLength : 0;
                segment.transform.position = _currentPosition;
                segment.AddComponent<Segment>();
                segment.GetComponent<Segment>().trackController = this;
            }
        }
    }

    public void LoadNextSegment() { LoadSegment(incrSegAmount); }
}
```

#### Segment 클래스
`Segment` 클래스는 트랙 세그먼트를 나타내며, 삭제 시 다음 세그먼트를 로드하도록 `TrackController`에 요청합니다.

```csharp
public class Segment : MonoBehaviour
{
    public TrackController trackController;

    private void OnDestroy()
    {
        if (trackController) trackController.LoadNextSegment();
    }
}
```

#### SegmentMarker 클래스
`SegmentMarker`는 플레이어가 트랙 세그먼트를 벗어날 때 해당 세그먼트를 삭제합니다.

```csharp
public class SegmentMarker : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<BikeController>() != null)
            Destroy(transform.parent.gameObject);
    }
}
```

#### Track 클래스
`Track` 클래스는 각 트랙의 세그먼트를 정의하는 ScriptableObject로, 세그먼트 목록과 세그먼트 간의 길이를 설정할 수 있습니다.

```csharp
[CreateAssetMenu(fileName = "New Track", menuName = "Track")]
public class Track : ScriptableObject
{
    public float segmentLength;
    public List<GameObject> segments = new List<GameObject>();
}
```

### 사용 예시
공간 분할 패턴은 대규모 공간을 관리해야 하는 레이싱 게임, 시뮬레이션, 그리고 실시간 전략 게임 등에서 자주 사용됩니다. 이 패턴을 통해 메모리와 CPU 성능을 최적화하여 게임의 부드러운 진행을 보장할 수 있습니다.

---

## Strategy Pattern (전략 패턴)

### 개요
전략 패턴은 여러 행동(전략)을 별도의 클래스에 캡슐화하여, 런타임에 어떤 전략을 사용할지 선택할 수 있게 해주는 디자인 패턴입니다. 이를 통해, 
같은 작업을 수행하지만 다른 방식으로 동작하는 여러 알고리즘을 쉽게 교환할 수 있습니다.

### 주요 클래스

- **Context**: 다양한 전략을 사용할 수 있는 클래스입니다. 이 예제에서는 `Drone`이 Context 역할을 합니다.
- **Strategy Interface**: 모든 구체적인 전략 클래스가 구현해야 하는 공통 인터페이스입니다. 이 예제에서는 `IManeuverBehaviour`가 전략 인터페이스 역할을 합니다.
- **Concrete Strategy**: 구체적인 전략 클래스이며, Context 객체에 적용할 수 있는 알고리즘 및 동작을 정의합니다. 이 예제에서는 `BoppingManeuver`, `WeavingManeuver`, `FallbackManeuver`가 이에 해당합니다.

### 장단점

#### 장점
- **캡슐화**: 개별 클래스에 알고리즘의 변형을 캡슐화하여 코드가 구조화된 상태를 유지하고 긴 조건문을 줄일 수 있습니다.
- **런타임 유연성**: 런타임에 객체가 사용할 전략을 교환할 수 있는 메커니즘을 제공하여 유연한 설계를 가능하게 합니다.

#### 단점
- **클라이언트 인지 필요**: 클라이언트가 사용할 전략을 직접 선택해야 하며, 전략의 구현 내용을 어느 정도 이해해야 합니다.
- **혼동 가능성**: 상태 패턴과 구조가 유사하여, 어떤 패턴을 사용할지 선택하는 데 혼동이 있을 수 있습니다.

### 코드 예시

#### IManeuverBehaviour 인터페이스
전략 인터페이스로, 구체적인 전략 클래스에서 구현해야 하는 메서드인 `Maneuver()`를 정의합니다.

```csharp
public interface IManeuverBehaviour
{
    void Manenuver(Drone drone);
}
```

#### Drone 클래스 (Context)
`Drone` 클래스는 `IManeuverBehaviour` 전략을 받아서 실행하며, 드론의 위치와 방향을 제어합니다.

```csharp
public class Drone : MonoBehaviour
{
    public float speed = 1.0f;

    public void ApplyStrategy(IManeuverBehaviour strategy)
    {
        strategy.Manenuver(this);
    }
}
```

#### BoppingManeuver 클래스 (Concrete Strategy)
`BoppingManeuver`는 드론이 상하로 움직이는 전략을 정의합니다.

```csharp
public class BoppingManeuver : MonoBehaviour, IManeuverBehaviour
{
    public void Manenuver(Drone drone)
    {
        StartCoroutine(Bopple(drone));
    }

    IEnumerator Bopple(Drone drone)
    {
        // 드론을 상하로 움직이는 코드
    }
}
```

#### FallbackManeuver 클래스 (Concrete Strategy)
`FallbackManeuver`는 드론이 뒤로 이동하는 전략을 정의합니다.

```csharp
public class FallbackManeuver : MonoBehaviour, IManeuverBehaviour
{
    public void Manenuver(Drone drone)
    {
        StartCoroutine(Fallback(drone));
    }
}
```

#### ClientStrategy 클래스
`ClientStrategy`는 드론을 생성하고, 랜덤한 전략을 선택하여 적용합니다.

```csharp
public class ClientStrategy : MonoBehaviour
{
    private GameObject _drone;

    private void SpawnDrone()
    {
        _drone = GameObject.CreatePrimitive(PrimitiveType.Cube);
        _drone.AddComponent<Drone>();
        ApplyRandomStrategies();
    }

    private void ApplyRandomStrategies()
    {
        _components.Add(_drone.AddComponent<WeavingManeuver>());
        int index = Random.Range(0, _components.Count);
        _drone.GetComponent<Drone>().ApplyStrategy(_components[index]);
    }
}
```

### 사용 예시
전략 패턴은 동작이 상황에 따라 변경되어야 할 때 유용합니다. 예를 들어, 게임 캐릭터의 AI 동작, 애니메이션 전환, 게임에서 서로 다른 공격 패턴 등을 쉽게 구현할 수 있습니다.

---

## Visitor Pattern (방문자 패턴)

### 개요
방문자 패턴의 주요 목적은 객체를 직접 수정하지 않고도 새로운 기능을 추가하는 것입니다. 이 패턴을 통해 객체의 구조를 유지하면서 기능을 확장할 수 있습니다. 객체의 내부 데이터 구조를 유지하면서 객체가 하는 일에 대한 책임을 방문자 클래스에 위임할 수 있습니다.

### 주요 클래스

- **Visitor Interface (IVisitor)**: 방문자 인터페이스이며, 방문할 수 있는 각 클래스에 대해 방문자 메서드를 정의합니다.
- **Concrete Visitor (PowerUp)**: 방문자 인터페이스를 구현하여 구체적인 행동을 정의합니다. 이 예제에서는 `PowerUp`이 방문자 역할을 하여 다양한 파워업 효과를 적용합니다.
- **Visitable Interface (IVisitorElement)**: 방문 가능한 요소 인터페이스입니다. 모든 방문 가능한 요소는 `Accept()` 메서드를 통해 방문자를 받아들이게 됩니다.
- **Concrete Elements (Shield, Shoose, Weapon)**: 구체적인 방문 대상 요소들이며, 각 요소는 `IVisitorElement`를 구현하여 방문자가 작업할 수 있도록 허용합니다.

### 장단점

#### 장점
- **개방/폐쇄 원칙 준수**: 객체를 수정하지 않고도 새로운 동작을 추가할 수 있어 객체 지향 원칙을 따릅니다.
- **단일 책임 원칙 준수**: 데이터 보유와 행동 추가의 책임을 분리하여 객체가 단일 책임 원칙을 준수할 수 있습니다.

#### 단점
- **접근성 문제**: 방문자는 방문하는 요소의 private 필드 및 메서드에 접근하기 어렵습니다.
- **복잡성 증가**: 패턴 구조가 복잡하여 코드베이스가 커질 수 있으며, 이해가 어렵고 유지보수에 부담이 될 수 있습니다.

### 코드 예시

#### Visitor Interface (IVisitor)
각 요소에 방문하는 메서드를 정의합니다.

```csharp
public interface IVisitor
{
    void Visit(Weapon weapon);
    void Visit(Shield shield);
    void Visit(Shoose shoose);
}
```

#### Concrete Visitor (PowerUp)
`PowerUp`은 `IVisitor`를 구현하여 방문할 때 적용할 효과를 정의합니다.

```csharp
[CreateAssetMenu(fileName = "PowerUP", menuName = "PowerUP")]
public class PowerUp : ScriptableObject, IVisitor
{
    public float Boost;
    public int weaponRange;
    public float weaponStrength;

    public void Visit(Weapon weapon)
    {
        weapon.range += weaponRange;
        weapon.strength += Mathf.Round(weapon.strength * weaponStrength / 100);
    }

    public void Visit(Shield shield) { shield.health = 100.0f; }

    public void Visit(Shoose shoose) { shoose.Boost += Boost; }
}
```

#### Visitable Interface (IVisitorElement)
모든 방문 가능한 요소가 구현해야 하는 인터페이스입니다.

```csharp
public interface IVisitorElement
{
    void Accept(IVisitor visitor);
}
```

#### Concrete Elements (Weapon, Shield, Shoose)
각 클래스는 `IVisitorElement`를 구현하여 `Accept()` 메서드를 통해 방문자가 작업을 수행하도록 합니다.

```csharp
public class Weapon : MonoBehaviour, IVisitorElement
{
    public int range = 5;
    public float strength = 25.0f;

    public void Accept(IVisitor visitor) { visitor.Visit(this); }
}
```

#### GameController (Client)
`GameController`는 여러 `IVisitorElement` 객체에 방문자를 전달하여 다양한 행동을 적용합니다.

```csharp
public class GameController : MonoBehaviour, IVisitorElement
{
    private List<IVisitorElement> _visitorElements = new List<IVisitorElement>();

    private void Start()
    {
        _visitorElements.Add(new Shield());
        _visitorElements.Add(new Shoose());
        _visitorElements.Add(new Weapon());
    }

    public void Accept(IVisitor visitor)
    {
        foreach (IVisitorElement element in _visitorElements)
        {
            element.Accept(visitor);
        }
    }
}
```

### 사용 예시
방문자 패턴은 객체의 구조를 유지하면서 새로운 동작을 추가해야 할 때 유용합니다. 예를 들어, 게임에서 다양한 파워업 효과를 플레이어에게 부여할 때 방문자 패턴을 사용하여 구조를 변경하지 않고 다양한 효과를 구현할 수 있습니다.
