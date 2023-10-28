// Powershell request

$response = Invoke-RestMethod -Uri "http://localhost:5005/efficient" -Method Get

if ($response) {
    Write-Output "Received item: $($response)"
} else {
    Write-Output "No content received"
}