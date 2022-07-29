# File Share API
Store images, and documents.

## TODO list:
This file contains a list of todo, and done tasks for the project.

### Registration
- [x] Implement endpoint for registering
- [x] Implement endpoint for registration confirmation 

### Authentication
- [x] JWT Bearer
    - [x] Setup swagger with bearer
    - [x] Implement login endpoint
    - [x] Implement jwt refresh token endpoint
    - [x] Implement endpoint for deletion of refresh token

- [ ] Password Management
    - [x] Implement change password
    - [ ] Implement forgot password
	- [ ] Implement password validation

### Account
- [ ] Manage account
    - [ ] Implement change username
    - [ ] Implement forgot username

### File upload
- [ ] Implement file management
    - [x] Implement file upload
    - [x] Implement file download
    - [x] Implement file soft deletion
    - [ ] Implement permanent auto deletion of soft deleted files

### Requests
- [ ] Implement rate limiting

### Unit Testing
- [x] Implement xUnit