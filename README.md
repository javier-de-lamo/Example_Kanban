# Example_Kanban

This is a very simple kanban example, programmed with ASP.NET Core REST API.
Its a Task control system.

Requirements:
Task start as 'ToDo'.
A 'ToDo' Task can be edited, moved to 'Doing', or deleted.
A 'Doing Task' can be edited, moved to 'Todo', or to 'Done'. Cannot be deleted.
A 'Done' Task cannot be edited or deleted, but can be moved back to 'Doing'.

The API will expose endpoints to make all this possible.

First version won't have authentication, but will have swagger documentation implemented.
Second version will introduce authentication and user management, like creation or deletion of users. Each user will have its own task.
Third version will introduce a frontend.
