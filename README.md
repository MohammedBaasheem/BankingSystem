 Banking System

The Banking System is a simple application designed to facilitate the management of bank accounts. This system allows users to add clients, open accounts, deposit money, withdraw money, transfer funds, and search for client and account information.

 Features

- **Add Clients**: Ability to add new clients to the system.
- **Open Accounts**: Clients can open new accounts (Current and Business Accounts).
- **Deposit Money**: Ability to deposit money into accounts.
- **Withdraw Money**: Ability to withdraw money from accounts, with restrictions on withdrawals for business accounts.
- **Transfer Money**: Ability to transfer funds between accounts.
- **Search for Clients and Accounts**: Search for client information and their accounts.
- **Display Client List**: Show all registered clients in the system.
- **Display Account List**: Show all open accounts.
- **Print Account Information by Specific Date**: Print information for accounts opened on a specific date.
- **Close Accounts**: Ability to close client accounts.

 Usage

1. **Install Requirements**: Make sure you have the .NET SDK installed on your machine.
2. **Run the Application**:
   - Open the command line or Terminal.
   - Navigate to the project folder.
   - Execute the following command:
     ```bash
     dotnet run
     ```
3. **Interact with the System**: Follow the on-screen instructions to use the various features.

 Project Structure

- **Program.cs**: Contains the main code for the system.
- **Account Class**: Represents a bank account and includes methods for withdrawal, deposit, and transfer.
- **CurrentAccount Class**: Represents a current account with specific withdrawal logic.
- **BusinessAccount Class**: Represents a business account with specific withdrawal logic.
- **Clients Class**: Represents a client and contains client information and associated accounts.
- **BANKINGSYSTEM Class**: Contains the logic for managing clients and accounts.

 Contribution

If you are interested in contributing to improve this project, feel free to open a Pull Request or provide suggestions.

 Notes

- It is recommended to read the code and understand its structure before using it.
- Be sure to review any additional requirements or changes that may arise in the project.
