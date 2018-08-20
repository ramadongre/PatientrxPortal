# PatientRxPortal
Patient Rx Portal

This portal allows doctor's office to manage patient prescription(Rx) information.

1. Office has the ability to add patients and rx information. Each Rx can have maximum of 5 prescriptions.
2. They have ability search patients by first and last name using the search box on the right side of landing screen.
3. They have the ability to update patient demographics and Rx information.
4. They would only see latest Rx information along with patient record in landing screen, but could see rest of the Rx in "Manage Rx" screen.

Technology
----------
C#
ASP.NET MVC
SQL Server
Boostrap (Responsive)
jQuery
Git

Benefits
---------------
Authentication,
Responsive,
N Tier Architecture,
Single page design

Known issues
------------------
1)In landing page, if you go from edit-patient and then back to add-patient twice, the add-patient button becomes un-responsive and same can be seen in manage-Rx screen. To overcome the issue, please refresh the parent page once.
2)Jquery Mask and date-picker do not relaibly bind to controls.
3)Date validations.

Possible improvement
--------------
1)Better testing.
2)Replace class library with web-api or WCF for tier communications.  
3)True server side pagination for patient and rx records.

Database scripts
--------
Found in "Database Scripts" folder of this location. This script creates database and provides a login user.

How recreate this example
-------------
1)Download the entire source code and run the webui project on local IIS or IIS express.
2)Use database scripts to create the database and login user.
3)In Web.config for WebUI project, update the database credentials for Entity framework.
4)Run the app. 

Demo
-----------
The application has been hosted on Azure VM & SQL:

http://52.170.151.71/PatientRxPortal/

Use admin/password to login.


