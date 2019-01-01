-- Auto-generated code below aims at helping you parse
-- the standard input according to the problem statement.

N = tonumber(io.read())

pow = {}

for i=0,N-1 do
    Pi = tonumber(io.read())
    pow[i] = Pi
end

table.sort(pow)

res = 100000000
for i=0,N-2 do
   if (pow[i+1] - pow[i] < res and pow[i+1] - pow[i] >= 0) then
       res = pow[i+1] - pow[i]
   end
end

-- Write an action using print()
-- To debug: io.stderr:write("Debug message\n")

print(res)