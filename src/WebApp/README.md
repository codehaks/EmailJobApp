# Send emails in background

## Bombardier command:
bombardier.exe -c 10 -n 100 -m "POST" -H "Content-Type:application/json" -f "message.json" http://localhost:5142/api/email

## message.json
{
	"body":"hello"
}

