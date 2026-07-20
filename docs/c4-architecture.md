# C4 Architecture: Wolfberry.TelldusLive

> To view the Mermaid diagrams in this document, open it in [privmark.app](https://privmark.app) — a privacy-focused markdown viewer that renders Mermaid diagrams locally.

## Level 1: System Context

Shows the system, its users, and external dependencies.

```mermaid
flowchart TB
    consumer["Consumer Application\n(.NET app using the SDK)"]
    library["Wolfberry.TelldusLive\nNuGet package\n180+ typed methods for the\nTelldus Live API"]
    telldusApi["Telldus Live API\napi.telldus.com\nCloud API for home automation"]
    telldusPortal["Telldus API Portal\napi.telldus.com/keys\nOAuth 1.0 credential management"]
    developer(["Developer\nObtains OAuth keys\nfrom the portal"])

    developer -- "Registers app,\nobtains keys" --> telldusPortal
    developer -- "Passes credentials\nto constructor" --> consumer
    consumer -- "Uses\n(.NET API calls)" --> library
    library -- "HTTPS + OAuth 1.0\n(GET requests)" --> telldusApi

    style library fill:#1168bd,color:#fff
    style telldusApi fill:#999,color:#fff
    style telldusPortal fill:#999,color:#fff
    style consumer fill:#438dd5,color:#fff
```

## Level 2: Container

In C4 terms, a "container" is a separately deployable/runnable unit.
This SDK is a library — it runs **in-process** with the consumer.
There is only one container boundary to draw.

```mermaid
flowchart TB
    subgraph consumerProcess["Consumer Process (single .NET process)"]
        consumerCode["Consumer Code"]
        subgraph nuget["Wolfberry.TelldusLive (NuGet package, netstandard2.0)"]
            sdkCore["SDK Core\nTelldusLiveClient\nRepositories, HTTP, Auth"]
        end
        subgraph deps["Third-Party Dependencies"]
            tinyOAuth["TinyOAuth1 v1.1.0\nOAuth 1.0 message signing"]
            newtonsoft["Newtonsoft.Json v13.0.4\nJSON serialization"]
        end
    end

    telldusApi["Telldus Live API\napi.telldus.com"]

    consumerCode --> sdkCore
    sdkCore --> tinyOAuth
    sdkCore --> newtonsoft
    sdkCore -- "HTTPS + OAuth 1.0" --> telldusApi

    style nuget fill:#1168bd,color:#fff
    style telldusApi fill:#999,color:#fff
    style tinyOAuth fill:#666,color:#fff
    style newtonsoft fill:#666,color:#fff
```

## Level 3: Component

Shows all components within the NuGet package and their relationships.

```mermaid
flowchart TB
    subgraph entrypoint["Entry Point"]
        liveClient["TelldusLiveClient\n\nITelldusLiveClient\nIDisposable\n\nCreates object graph\nValidates credentials"]
    end

    subgraph config["Configuration"]
        oauthConfig["TelldusOAuth1Configuration\n\nConsumerKey, ConsumerKeySecret\nAccessToken, AccessTokenSecret\nOAuth endpoint URLs"]
        configEx["ConfigurationException\n\nThrown on missing/invalid\ncredentials at construction"]
    end

    subgraph repositories["Domain Repositories"]
        baseRepo["BaseRepository\n\nGetOrThrow&lt;T&gt;(url)\nShared error handling\nand deserialization"]
        clientRepo["ClientRepository\nIClientRepository\n9 methods"]
        deviceRepo["DeviceRepository\nIDeviceRepository\n22 methods"]
        eventRepo["EventRepository\nIEventRepository\n27 methods"]
        groupRepo["GroupRepository\nIGroupRepository\n2 methods"]
        schedulerRepo["SchedulerRepository\nISchedulerRepository\n4 methods"]
        sensorRepo["SensorRepository\nISensorRepository\n9 methods"]
        userRepo["UserRepository\nIUserRepository\n17 methods"]
    end

    subgraph infrastructure["Infrastructure"]
        httpClient["TelldusHttpClient\n\nITelldusHttpClient\nIDisposable\n\nExecutes HTTP calls\nReturns raw JSON"]
        authenticator["Authenticator\n\nIAuthenticator\nIDisposable\n\nOAuth 1.0 signing\nOwns HttpClient"]
        errorParser["ErrorParser\n\nstatic\n\nDetects errors in JSON,\nnon-JSON, and null responses"]
        urlBuilder["UrlBuilder\n\nBuilds URLs with query\nparams and URI escaping"]
        jsonUtil["JsonUtil\n\nstatic\n\nNewtonsoft.Json wrapper"]
    end

    subgraph external["External (Third-Party)"]
        tinyOAuth["TinyOAuth1\nOAuth 1.0 protocol"]
        newtonsoft["Newtonsoft.Json\nJSON serialization"]
    end

    subgraph exceptions["Exceptions"]
        repoEx["RepositoryException\nThrown on API errors"]
    end

    liveClient -- "validates" --> oauthConfig
    oauthConfig -. "invalid" .-> configEx
    liveClient -- "creates (one shared instance)" --> httpClient
    liveClient -- "creates all 7, injects shared httpClient" --> clientRepo

    clientRepo -- "extends" --> baseRepo
    deviceRepo -- "extends" --> baseRepo
    eventRepo -- "extends" --> baseRepo
    groupRepo -- "extends" --> baseRepo
    schedulerRepo -- "extends" --> baseRepo
    sensorRepo -- "extends" --> baseRepo
    userRepo -- "extends" --> baseRepo

    clientRepo -- "builds URLs" --> urlBuilder
    deviceRepo -- "builds URLs" --> urlBuilder
    eventRepo -- "builds URLs" --> urlBuilder
    groupRepo -- "builds URLs" --> urlBuilder
    schedulerRepo -- "builds URLs" --> urlBuilder
    sensorRepo -- "builds URLs" --> urlBuilder
    userRepo -- "builds URLs" --> urlBuilder

    baseRepo -- "GetAsJsonAsync" --> httpClient
    baseRepo -- "parse errors" --> errorParser
    baseRepo -- "deserialize" --> jsonUtil
    errorParser -. "error detected" .-> repoEx

    httpClient -- "delegates to" --> authenticator
    authenticator -- "signs requests" --> tinyOAuth
    jsonUtil -- "wraps" --> newtonsoft

    style liveClient fill:#1168bd,color:#fff
    style httpClient fill:#1168bd,color:#fff
    style authenticator fill:#1168bd,color:#fff
    style baseRepo fill:#2694ab,color:#fff
    style clientRepo fill:#2694ab,color:#fff
    style deviceRepo fill:#2694ab,color:#fff
    style eventRepo fill:#2694ab,color:#fff
    style groupRepo fill:#2694ab,color:#fff
    style schedulerRepo fill:#2694ab,color:#fff
    style sensorRepo fill:#2694ab,color:#fff
    style userRepo fill:#2694ab,color:#fff
    style tinyOAuth fill:#666,color:#fff
    style newtonsoft fill:#666,color:#fff
    style configEx fill:#c44,color:#fff
    style repoEx fill:#c44,color:#fff
```

## Level 4: Code

### Interface hierarchy

```mermaid
classDiagram
    class ITelldusLiveClient {
        <<interface>>
        +IClientRepository Clients
        +IDeviceRepository Devices
        +IEventRepository Events
        +IGroupRepository Groups
        +ISchedulerRepository Scheduler
        +ISensorRepository Sensors
        +IUserRepository User
        +Dispose()
    }

    class ITelldusHttpClient {
        <<interface>>
        +string BaseUrl
        +GetResponseAsType~T~(url) Task~T~
        +GetAsJsonAsync(uri) Task~string~
        +Dispose()
    }

    class IAuthenticator {
        <<interface>>
        +HttpClient HttpClient
        +InitializeHttpClient()
        +GetAuthorizationUrlAsync() Task~string~
        +FinalizeAuthorizationAsync() Task~AccessTokenInfo~
        +Dispose()
    }

    class BaseRepository {
        #ITelldusHttpClient _httpClient
        #GetOrThrow~T~(url) Task~T~
    }

    class TelldusLiveClient {
        -ITelldusHttpClient _httpClient
        -ValidateConfiguration()
        +Dispose()
    }

    ITelldusLiveClient <|.. TelldusLiveClient
    TelldusLiveClient *-- ITelldusHttpClient : owns
    ITelldusHttpClient <|.. TelldusHttpClient
    TelldusHttpClient *-- IAuthenticator : owns
    IAuthenticator <|.. Authenticator
    Authenticator *-- HttpClient : owns

    BaseRepository --> ITelldusHttpClient : uses
    BaseRepository --> ErrorParser : uses
    BaseRepository --> JsonUtil : uses
    BaseRepository <|-- ClientRepository
    BaseRepository <|-- DeviceRepository
    BaseRepository <|-- EventRepository
    BaseRepository <|-- GroupRepository
    BaseRepository <|-- SchedulerRepository
    BaseRepository <|-- SensorRepository
    BaseRepository <|-- UserRepository
```

### Disposal chain (ownership)

```mermaid
flowchart LR
    A["TelldusLiveClient\nDispose()"] -- "disposes" --> B["TelldusHttpClient\nDispose()"]
    B -- "disposes" --> C["Authenticator\nDispose()"]
    C -- "disposes" --> D["HttpClient\n(socket handles)"]

    style A fill:#1168bd,color:#fff
    style B fill:#1168bd,color:#fff
    style C fill:#1168bd,color:#fff
    style D fill:#c44,color:#fff
```

## Request Flow (Happy Path)

```mermaid
sequenceDiagram
    participant C as Consumer
    participant R as SensorRepository
    participant B as BaseRepository
    participant U as UrlBuilder
    participant H as TelldusHttpClient
    participant A as Authenticator (OAuth 1.0)
    participant T as Telldus Live API

    C->>R: GetSensorsAsync(includeIgnored, includeValues)
    R->>U: new UrlBuilder(baseUrl + "/json/sensors/list")
    R->>U: AddQuery("includeIgnored", 1)
    R->>U: AddQuery("includeValues", 1)
    U-->>R: url
    R->>B: GetOrThrow<TelldusSensorsResponse>(url)
    B->>H: GetAsJsonAsync(url)
    H->>A: HttpClient.GetAsync(url)
    Note over A: OAuth 1.0 signature added<br/>via TinyOAuthMessageHandler
    A->>T: GET /json/sensors/list?includeIgnored=1&includeValues=1
    T-->>A: 200 OK + JSON body
    A-->>H: HttpResponseMessage
    H-->>B: JSON string
    B->>B: ErrorParser.GetOrCreateErrorMessage(json)
    Note over B: Returns null (no error)
    B->>B: JsonUtil.Deserialize<TelldusSensorsResponse>(json)
    B-->>R: TelldusSensorsResponse
    R-->>C: SensorsResponse
```

## Error Flow

```mermaid
sequenceDiagram
    participant C as Consumer
    participant R as DeviceRepository
    participant B as BaseRepository
    participant H as TelldusHttpClient
    participant T as Telldus Live API

    C->>R: TurnOnAsync("999")
    R->>B: GetOrThrow<StatusResponse>(url)
    B->>H: GetAsJsonAsync(url)
    H->>T: GET /json/device/turnOn?id=999
    T-->>H: {"error": "Device \"999\" not found!"}
    H-->>B: JSON string
    B->>B: ErrorParser.GetOrCreateErrorMessage(json)
    Note over B: Error field detected
    B->>B: throw new RepositoryException(errorMessage)
    B--xC: RepositoryException

    Note over C: Consumer catches<br/>RepositoryException
```

## Telldus API Domain Mapping

Shows which repository maps to which Telldus Live API URL namespace.

```
Repository              API path prefix         Methods   Domain
--------------------    --------------------    -------   ---------------------------------
ClientRepository        /client/* /clients/*          9   Gateways/controllers (ZNet Lite)
DeviceRepository        /device/* /devices/*         22   Lights, switches, dimmers, blinds
EventRepository         /event/*  /events/*          27   Automation rules, triggers, actions
GroupRepository         /group/*                      2   Device groups (deprecated)
SchedulerRepository     /scheduler/*                  4   Scheduled jobs (time/sun-based)
SensorRepository        /sensor/* /sensors/*          9   Temperature, humidity, etc.
UserRepository          /user/*                      17   Profile, push tokens, EULA, auth
```
