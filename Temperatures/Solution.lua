-- Auto-generated code below aims at helping you parse
-- the standard input according to the problem statement.

n = tonumber(io.read()) -- the number of temperatures to analyse
next_token = string.gmatch(io.read(), "[^%s]+")

res = 0
curDif = 5527

for i=0,n-1 do
    -- t: a temperature expressed as an integer ranging from -273 to 5526
    t = tonumber(next_token())
    
    if (t > 0) then
        if (t < curDif) then
            res = t
            curDif = t
        elseif (t == -res) then
            res = t
        end
    elseif (t == 0) then 
        res = t
        break
    else 
        if (-t < curDif) then
            if (-t ~= res) then
                res = t
                curDif = -t
            end
        end
    end
end

-- Write an action using print()
-- To debug: io.stderr:write("Debug message\n")

print(res)