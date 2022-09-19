
$file_data = Get-Content C:\Users\raghuramc\Documents\machineName.txt

$file_data.Count

for ($num = 1 ; $num -le $file_data.Count ; $num++)
{
$Hive = [Microsoft.Win32.RegistryHive]("LocalMachine");

$regKey = [Microsoft.Win32.RegistryKey]::OpenRemoteBaseKey([Microsoft.Win32.RegistryHive]("LocalMachine"),$file_data[$num]);

$ref = $regKey.OpenSubKey("SOFTWARE\ManageEngine\ADAP");

$ref.GetValue('AgentGuid')

if (!$ref) {$false}

else {$true}
}

//File name change in file_data and will return whether agent reg is available