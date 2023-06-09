# DTE Common
## Table of Contents
- [DTE Common](#dte-common)
  - [Project Description](#project-description)
  - [How to Install and Run the Project](#how-to-install-and-run-the-project)
  - [How to Use the Project](#how-to-use-the-project)

## Project Description
DTE Common is a C#-based utility library for commonly used interfaces and implementations in application development. The project focuses on providing a collection of standard, reusable code components such as an API client, a Clock interface, a PollyRetryService for resilient HTTP request handling, a Serializer for object serialization, and a TraceWriter for output tracing.

The utility library has been developed using the .NET Core framework and is intended to be used in various types of applications including serverless architectures, microservices, and web APIs. By leveraging these common components, developers can reduce redundancy, improve code readability, and increase efficiency in their application development process.

## How to Install and Run the Project
Follow these steps to set up and use the DTE Common library in your local development environment:

1. Ensure you have the .NET SDK installed on your machine. If not, download and install it from the [.NET official website](https://dotnet.microsoft.com/download).

2. Clone the repository to your local machine:

    ```bash
    git clone https://github.com/yourusername/dte-common.git
    ```

3. Navigate to the project folder:

    ```bash
    cd dte-common
    ```

4. Restore the required dependencies and build the project:

    ```bash
    dotnet restore
    dotnet build
    ```
5. Now the DTE Common library can be referenced in your .NET projects by adding a reference to the built dll or by directly including the project in your solution.

## How to Use the Project
The DTE Common project is a utility library which you can reference in your .NET applications. Each of the provided interfaces has its own specific use cases:

- `IApiClient`: This is used for making HTTP requests and can be used wherever there's a need to communicate with external APIs.
- `IClock`: This can be used to fetch the current date and time.
- `IPollyRetryService`: This provides resilient HTTP request handling with retries, useful when dealing with unreliable networks or services.
- `ISerializer`: This can be used to serialize objects to strings and vice versa, which is particularly useful when dealing with data storage or network communication.
- `ITraceWriter`: This is used for output tracing, which can help in logging and debugging.

Each interface may have its own usage nuances and these should be used according to the specific needs of your application.
