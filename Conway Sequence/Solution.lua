function ConvertToSequence (R, L, index)
	ind = index
    res = ""
    split = { count = 0 }
    for part in R:gmatch("%w+") do
        split[#split+1] = part
        io.stderr:write(split[#split].."\n")
        split.count = split.count + 1
    end
    io.stderr:write("split.count "..split.count.."\n")
    io.stderr:write("R "..R.."\n")
    count = 1
    current = split[1]
    io.stderr:write("Current "..current.."\n")
    for i = 2, split.count do
        io.stderr:write("count "..count.."\n")
		if split[i] ~= current then
			res = res..count.." "..current.." "
			current = split[i]
			count = 1
		else
			count = count + 1
        end
    end
    res = res..count.." "..current;
    io.stderr:write("Index "..ind.."\nResult "..res)
    ind = ind + 1
    if ind >= L then
        return res
    else
        return ConvertToSequence(res, L, ind)
    end
end

R = tonumber(io.read())
L = tonumber(io.read())

io.stderr:write("R "..tostring(R).."\nL "..L.."\n")	
result = ""
if L == 1 then 
	result = R
else 
	result = ConvertToSequence(tostring(R), L, 1)
end
-- Write an action using Console.WriteLine()
-- To debug: Console.Error.WriteLine("Debug messages...");

print(result)