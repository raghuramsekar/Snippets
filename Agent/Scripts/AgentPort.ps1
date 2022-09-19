$server = $args[0]
$argsCount = 1
$ports = 8081,8444,8555,445,135
try{
if($args.count -eq 0)
{
	Write-Host "Enter Machine Name as args,For now machine name is localhost"
	$server = "localhost"
}
while($argsCount -ne $args.count -and $args.count -ne 0)
{
 $argsCount = $argsCount+1
 $ports += $args[$argsCount]
}
foreach($port in $ports)
{
#    Write-host $port
    Test-NetConnection -ComputerName $server -Port $port | Select-Object -Property RemotePort,TcpTestSucceeded
}
}catch
{
	$_.exception.message
	continue
}

//Enter serverName as args
//If needed other than ports listed then enter it as subquent ports