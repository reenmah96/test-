# test-
Remember Server Name .
Create Table in SSMS , Insert and Select Tables .
In VS, View -> Server Explorer-> Data Connection Add-> Connect the Connection . 
In Web Config File in Solution Explorer : 
<connectionStrings>
		<add name="Myconnection" connectionString="Data Source=DESKTOP-V3TVV71;Initial Catalog=TB;Integrated Security=True" providerName="System.Data.SqlClient"/>
	</connectionStrings>


Backend –
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


