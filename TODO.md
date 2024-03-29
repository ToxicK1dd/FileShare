# File Share API
Store images, and documents.

## TODO list:
This file contains a list of todo, and done tasks for the project.

### Registration
- [x] Implement endpoint for registering
- [x] Implement endpoint for registration confirmation 

### Authentication
- [x] JWT
    - [x] Setup swagger with JWT
    - [x] Implement login endpoint
    - [x] Implement jwt refresh token endpoint
    - [x] Implement endpoint for deletion of refresh token
    - [x] Implement hashing of refresh tokens

- [ ] Password Management
    - [x] Implement change password
	- [x] Implement password validation
    - [ ] Implement forgot password

- [ ] TOTP Two-Factor
    - [x] Implement enable/disable 2FA
	- [x] Implement generate qrcode key 
	- [x] Implement recovery codes

### Account
- [ ] Manage account
    - [ ] Implement change username
    - [ ] Implement forgot username

### File upload
- [ ] Implement file management
    - [x] Implement file upload
    - [x] Implement file download
    - [x] Implement file soft deletion
	- [x] Implement file compression, and decompression
    - [ ] Implement permanent auto deletion of soft deleted files

### Requests
- [x] Implement rate limiting

### Unit Testing
- [x] Implement xUnit