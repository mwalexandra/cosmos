# Use of the Project

The project provides a set of commands for user and list management, as well as system administration. Below are the main commands that can be used in various administrative areas:

## User Management:
- **'userlist'** — displays all users.
- **'create'** — creates a new user and password.
- **'delete'** — deletes a user.
- **'rename'** — renames a user.
- **'repass'** — changes the user's password.
- **'rerole'** — changes the user's role.

## List Management:
- **'createlist'** — creates a new user list.
- **'deletelist'** — stops the program.
- **'accesslist'** — restarts the system.
- **'movelist'** — stops the program.
- **'editlist'** — restarts the system.
- **'showlist'** — restarts the system.
- **'changeuser'** — switches the user.

## System Administration:
- **'exit'** — exits the program.
- **'reboot'** — restarts the system.

Each command that modifies the system requires a check of the current user's permissions, which is taken into account in the program logic.

## Security and Permissions
Depending on the current user's access level, certain actions are permitted. To verify permissions, the system prompts for the current user's password with every change.





