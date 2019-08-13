-- Don't let the machines win. You are humanity's last hope...

width = tonumber(io.read()) -- the number of cells on the X axis
height = tonumber(io.read()) -- the number of cells on the Y axis
grid = {}
result = ''

for i=0,height-1 do
    line = io.read() -- width characters, each either 0 or .
    grid[i] = line
end

for i = 0, height do
    for i2 = 0, width do
        io.stderr:write("\n\n"..i.."\n\n")
        io.stderr:write(grid[i])
        if grid[i][i2] == '0' then
            result = result..i2.." "..i
                if i == height - 1 and i2 == width - 1 then
                    result = result.."-1 -1 -1 -1\n"
                end
                found = false
                if i2 == width - 1 then
                    result = result.."-1 -1 "
                    for i3 = i + 1, height do
                        if grid[i3][i2] == '0' then
                            result = result..i2.." "..i3   
                            found = true
                            break
                        end
                    end
                    if not found then
                        result = result.."-1 -1 "
                    end
                    result = result:sub(1, -1).."\n"
                end
                found = false
                if i == height - 1 then
                    for i3 = i2 + 1, width do
                        if grid[i][i3] == '0' then
                            result = result..i3.." "..i
                            found = true
                            break
                        end
                    end
                    if not found then
                        result = result.."-1 -1 "
                    end
                    result = result.."-1 -1 ";
                    result = result:sub(1, -1).."\n"
                end
                found = false;
                for i3 = i2 + 1, width do
                    if grid[i][i3] == '0' then
                        result = result..i3..i
                        found = true
                        break
                    end
                end
                if not found then
                    result = result.."-1 -1 "
                end
                found = false
                for i3 = i + 1, height do
                    if grid[i3][i2] == '0' then
                        result = result..i2..i3   
                        found = true
                        break
                    end
                end
                if not found then
                    result = result.."-1 -1 "
                end
                result = result:sub(1, -1)
            end
        end
    end
-- Write an action using print()
-- To debug: io.stderr:write("Debug message\n")


-- Three coordinates: a node, its right neighbor, its bottom neighbor
print(result)