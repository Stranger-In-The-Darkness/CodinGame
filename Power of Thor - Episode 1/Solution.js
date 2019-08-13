var i = readline().split(' ');
var x = parseInt(i[0]);
var y = parseInt(i[1]);
var X = parseInt(i[2]);
var Y = parseInt(i[3]);
while(true)
{
	readline();
	var d="";
	if (y > Y)
	{
		d += "S";
		Y++;
		if (x > X)
		{ 
			d+="E";
			X++;
		}
		else if (x < X)
		{
			d += "W";
			X--;
		}
	}
	else if (y < Y)
	{
		d += "N";
		Y--;
		if (x > X)
		{
			d += "E";
			X++;
		}
		else if (x < X)
		{
			d += "W";
			X--;
			}
		}
	else
	{
		if (x > X)
		{
			d += "E";
			X++;
		}
		else if (x < X)
		{
			d += "W";
			X--;
		}
	}
	console.log(d);
}