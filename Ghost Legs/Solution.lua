
next_token = string.gmatch(io.read(), "[^%s]+")
W = tonumber(next_token())
H = tonumber(next_token())
lanes = {}
answ = {}
for i = 0, H - 1 do
    lanes[i] = {}
	for j = 0, W - 1 do
		lanes[i][j] = ''
	end
end
for i = 0, H - 1 do
    line = io.read()
	for l = 0, #line - 1 do
		lanes[i][l] = line:sub(l + 1, l + 1)
	end
end
for i = 0, W - 1, 3 do
	answ[#answ+1] = lanes[0][i]
	--print(answ[#answ+1])
	li = i
	for j=0,H-1 do
		if li==0 then
			if lanes[j][li+1] == '-' then
				io.stderr:write(li.."\n")
				li = li + 3
			end
		elseif li == W-1 then
			if lanes[j][li-1] == '-' then
				io.stderr:write(li.."\n")
				li = li - 3
			end
		else
			if lanes[j][li+1] == '-' then
				io.stderr:write(li.."\n")
				li = li+3
			elseif lanes[j][li-1] == '-' then
				io.stderr:write(li.."\n")
				li = li-3
			end
		end
	end
	answ[#answ] = answ[#answ]..lanes[H-1][li]
end

for i = 1, #answ do
	print(answ[i])
end