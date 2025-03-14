
# Following steps are used to register your .Net Core API  using Azure AD and then authenticate it via Postman

- Navigate to portal.azure.com
- Search for feature termed as Microsoft EntraId
- Locate AppRegistration and click on it
  . ![Local Image](/AppRegistration/assets/1stImage.png)
- Click of new Registration
   . ![Local Image](/AppRegistration/assets/NewRegistration.png)
- Click on Register
  . ![Local Image](/AppRegistration/assets/RegisterScreen.png)
- Once register is successful, click on Certificates & Secrets
  ![Local Image](/AppRegistration/assets/CertificateAndSecret.png)
- Click on  **Expose an API** and add scope
 ![Local Image](AppRegistration/assets/ExposeAPI.png)
 ![Local Image](AppRegistration/assets/AddScope.png)
- Once these changes are done, you can go to **Overview** page and copy these
-  ![Local Image](AppRegistration/assets/Endpoints.png)
- Open Postman , Go to Authorization Tab and select AuthType as **OAuth2.0**
- ![Local Image](AppRegistration/assets/Postman.png)