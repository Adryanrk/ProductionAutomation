# ProductionAutomation

ProductionAutomation is a simulated IT/OT integration system developed as part of the **Automation and Integration** course.

The project demonstrates how production orders can be automatically transferred from an IT system to a simulated OT environment, processed, and reported back without manual intervention.

## Overview

The solution consists of three main applications:

- **OrderSystem** – Simulates the IT side of the environment. Users can create and view production orders stored in a SQLite database.
- **IntegrationService** – Monitors the database for pending orders and automatically sends them to the OT system.
- **OTSystem** – Simulates an industrial production system that receives and processes production orders.

A separate **xUnit test project** is included for automated unit and integration testing.

## Architecture

![System Architecture](docs/images/architecture.png)

The automated workflow follows this process:

1. A production order is created in OrderSystem.
2. The order is stored in SQLite with the status `Pending`.
3. IntegrationService detects the order automatically.
4. The status is changed to `Processing`.
5. The order is serialized to JSON and sent to OTSystem over TCP.
6. OTSystem simulates production from 25% to 100%.
7. OTSystem returns the production result.
8. IntegrationService updates the order to `Completed`.
9. OrderSystem displays the updated status.

## Technologies

- C#
- .NET 9
- Entity Framework Core
- SQLite
- TCP/IP
- JSON
- xUnit
- Git / GitHub

## Key Features

- Automated IT/OT order flow
- Shared SQLite database
- TCP-based communication between IntegrationService and OTSystem
- JSON serialization
- Order validation
- Order status tracking (`Pending`, `Processing`, `Completed`, `Failed`)
- Network error handling
- Automatic retry behavior when OTSystem is unavailable
- Unit and integration testing

## Error Handling

The system is designed to handle temporary communication failures without incorrectly completing an order.

If OTSystem is unavailable, IntegrationService catches the connection error and returns the affected order to `Pending`. This allows the order to be retried when the OT system becomes available again.

## Testing

The solution contains a dedicated xUnit project, `ProductionAutomation.Tests`.

The automated test suite includes:

- 6 unit tests for order validation
- 3 integration tests for OrderService and SQLite
- 9 automated tests in total

All automated tests passed successfully during the final test run.

![Automated Tests](docs/images/tests-passed.png)

The complete system was also verified through manual end-to-end and negative testing.

## Running the Project

The applications should be started in the following order:

1. **OTSystem**
2. **IntegrationService**
3. **OrderSystem**

OTSystem listens locally on:

`127.0.0.1:5000`

IntegrationService continuously monitors the shared SQLite database for pending production orders.

Once all three applications are running, create a new order in OrderSystem. The remaining production flow is handled automatically.

## Security Considerations

This project is a local simulation and is not intended for use in a real production environment.

The current TCP communication is not encrypted or authenticated. A production-ready implementation should include measures such as TLS encryption, machine-to-machine authentication, authorization, secure logging, network segmentation, firewall rules, and restricted database permissions.

## Project Structure

```text
ProductionAutomation/
├── ProductionAutomation/
├── docs/
│   └── images/
├── .gitattributes
├── .gitignore
├── ProductionAutomation.sln
├── README.md
└── Rapport.md
```

## Documentation

For a detailed description of the architecture, implementation, security analysis, testing, and results, see the full project report:

**[Project Report (Swedish)](Rapport.md)**

## Author

**Adryan Rajabi**

Developed as part of the *Automation and Integration* course.
