-- Auto-generated code below aims at helping you parse
-- the standard input according to the problem statement.
function Reverse(from, to) do
    io.stderr:write(string.format("from %s to %s\n", from, to))
        str = ""
        i = 1
        while true do
            count = 1
            for j = i, from:len()-1 do
                if from:sub(j, j) == from:sub(j+1, j+1) then
                    count = count + 1
                else
                    break
                end
            end
            --io.stderr:write(string.format("count = %d part = %s str = %s\n", count, from:sub(i, i), str))
            str = str .. tostring(count) .. from:sub(i, i)
            --io.stderr:write(string.format("i = %d %s count = %d str = %s\n", i, tostring(i>from:len()), count, str))
            i = i + count
            if i>from:len() then break end
            --io.stderr:write(tostring(i).."\n")
        end
        io.stderr:write(string.format("from = %s str = %s to = %s\n\n", from,  str, to))
        if str ~= to then return false end
        return true
    end
end

s = io.read()

res = s;
while res:len() % 2 == 0 do
    r = ""
    count = 0
    for i = 1, res:len(), 2 do
        count = tonumber(res:sub(i, i))
        r = r..string.rep(tostring(res:sub(i+1, i+1)), count)
    end
    if res == r then 
        break 
    end
    io.stderr:write(string.format("%s %s %s\n", tostring(Reverse(r, res)), res, r))
    if Reverse(r, res) then
        res = r
    else 
        break
    end
end

print(res)