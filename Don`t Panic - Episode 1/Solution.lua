-- Auto-generated code below aims at helping you parse
-- the standard input according to the problem statement.

-- nbFloors: number of floors
-- width: width of the area
-- nbRounds: maximum number of rounds
-- exitFloor: floor on which the exit is found
-- exitPos: position of the exit on its floor
-- nbTotalClones: number of generated clones
-- nbAdditionalElevators: ignore (always zero)
-- nbElevators: number of elevators
next_token = string.gmatch(io.read(), "[^%s]+")
nbFloors = tonumber(next_token())
width = tonumber(next_token())
nbRounds = tonumber(next_token())
exitFloor = tonumber(next_token())
exitPos = tonumber(next_token())
nbTotalClones = tonumber(next_token())
nbAdditionalElevators = tonumber(next_token())
nbElevators = tonumber(next_token())

level = {}
for i = 0,nbFloors -1 do
    level[i] = {}
    for i2 = 0, width-1 do
        level[i][i2] = ' '
    end
end

level[exitFloor][exitPos] = 'e'

for i=0,nbElevators-1 do
    -- elevatorFloor: floor on which this elevator is found
    -- elevatorPos: position of the elevator on its floor
    next_token = string.gmatch(io.read(), "[^%s]+")
    elevatorFloor = tonumber(next_token())
    elevatorPos = tonumber(next_token())
    level[elevatorFloor][elevatorPos] = 'l'
end

-- game loop
while true do
    -- cloneFloor: floor of the leading clone
    -- clonePos: position of the leading clone on its floor
    -- direction: direction of the leading clone: LEFT or RIGHT
    next_token = string.gmatch(io.read(), "[^%s]+")
    cloneFloor = tonumber(next_token())
    clonePos = tonumber(next_token())
    direction = next_token()
    
    done = false
    
    -- Write an action using print()
    -- To debug: io.stderr:write("Debug message\n")
    
    if cloneFloor == -1 
            and clonePos == -1
            and direction == "NONE" then
        print("WAIT")
        io.stderr:write("60\n")
        done = true
    end
    if cloneFloor == exitFloor and not done then
        if clonePos > exitPos then
            if direction == "LEFT" then
                if not done then
                    print("WAIT")
                    io.stderr:write("67\n")
                    done = true
                end
            elseif direction == "RIGHT" then
                if exitFloor > 0 then
                    if level[exitFloor - 1][clonePos] ~= 'l' then
                        if not done then
                            print("BLOCK")
                            io.stderr:write("76\n")
                            done = true
                        end
                    else 
                        if not done then
                            print("WAIT")
                            io.stderr:write("82\n")
                            done = true
                        end
                    end
                else 
                    if not done then
                        print("BLOCK")
                        io.stderr:write("89\n")
                        done = true
                    end
                end
            end
        elseif clonePos < exitPos then
            if direction == "RIGHT" then
                if not done then
                    print("WAIT");
                    io.stderr:write("98\n")
                    done = true
                end
            elseif direction == "LEFT" then
                if exitFloor > 0 then
                    if level[exitFloor - 1][clonePos] ~= 'l' then
                        if not done then
                            print("BLOCK")
                            io.stderr:write("106\n")
                            done = true
                        end
                    else
                        if not done then
                            print("WAIT")
                            io.stderr:write("112\n")
                            done = true
                        end
                    end
                else 
                    if not done then
                        print("BLOCK")
                        io.stderr:write("119\n")
                        done = true
                    end
                end
            else
                if not done then
                    print("WAIT")
                    io.stderr:write("126\n")
                    done = true
                end
            end
        end
            
        if (clonePos == width-1 and level[cloneFloor][clonePos] ~= 'e')
            or (clonePos == 0 and level[cloneFloor][clonePos] ~= 'e') then
                
            if (direction == "LEFT" and (clonePos - exitPos) > 0) 
            or (direction == "RIGHT" and (clonePos - exitPos) < 0) then
                if not done then
                    print("WAIT")
                    io.stderr:write("139\n")
                    done = true
                end
            else
                if not done then
                    print("BLOCK")
                    io.stderr:write("145\n")
                    done = true
                end
            end
        else
            index = -1
            for i = 0,width-1 do
                if cloneFloor >= 0 then
                    if level[cloneFloor][i] == 'l' then
                        index = i
                        break
                    end
                end
            end
            if index == clonePos then
                io.stderr:write("==\n")
                if not done then
                    print("WAIT")
                    io.stderr:write("160\n")
                    done = true
                end
            elseif index > clonePos then
                io.stderr:write(">\n")
                if direction == "RIGHT" then
                    if not done then
                        print("WAIT")  
                        io.stderr:write("167\n")
                        done = true
                    end
                elseif direction == "LEFT" then
                    if cloneFloor > 0 then
                        if level[cloneFloor -1][clonePos] == 'l' then
                            if not done then
                                print("WAIT")
                                io.stderr:write("175\n")
                                done = true
                            end
                        else
                            if not done then
                                print("BLOCK")
                                io.stderr:write("181\n")
                                done = true
                            end
                        end
                    else
                        if not done then
                            print("BLOCK")
                            io.stderr:write("188\n")
                            done = true
                        end
                    end
                else
                    if direction == "LEFT" then
                        if not done then
                            print("WAIT")
                            io.stderr:write("196\n")
                            done = true
                        end
                    elseif direction == "RIGHT" then
                        if cloneFloor > 0 then
                            if level[cloneFloor -1][clonePos] == 'l' then
                                if not done then
                                    print("WAIT")
                                    io.stderr:write("204\n")
                                    done = true
                                end
                            else
                                if not done then
                                    print("BLOCK")
                                    io.stderr:write("210\n")
                                    done = true
                                end
                            end
                        else
                            if not done then
                                print("BLOCK")
                                io.stderr:write("217\n")
                                done = true
                            end
                        end
                    end
                end
            else
                io.stderr:write("<\n")
                if direction == "LEFT" then
                    if not done then
                        print("WAIT")  
                        io.stderr:write("227\n")
                        done = true
                    end
                elseif direction == "RIGHT" then
                    if cloneFloor > 0 then
                        if level[cloneFloor -1][clonePos] == 'l' then
                            if not done then
                                print("WAIT")
                                io.stderr:write("235\n")
                                done = true
                            end
                        else
                            if not done then
                                print("BLOCK")
                                io.stderr:write("241\n")
                                done = true
                            end
                        end
                    else
                        if not done then
                            print("BLOCK")
                            io.stderr:write("248\n")
                            done = true
                        end
                    end
                else
                    if direction == "RIGHT" then
                        if not done then
                            print("WAIT")
                            io.stderr:write("256\n")
                            done = true
                        end
                    elseif direction == "LEFT" then
                        if cloneFloor > 0 then
                            if level[cloneFloor -1][clonePos] == 'l' then
                                if not done then
                                    print("WAIT")
                                    io.stderr:write("264\n")
                                    done = true
                                end
                            else
                                if not done then
                                    print("BLOCK")
                                    io.stderr:write("270\n")
                                    done = true
                                end
                            end
                        else
                            if not done then
                                print("BLOCK")
                                io.stderr:write("217\n")
                                done = true
                            end
                        end
                    end
                end
            end
        end
    else
        if (clonePos == width-1 and level[cloneFloor][clonePos] ~= 'e')
            or (clonePos == 0 and level[cloneFloor][clonePos] ~= 'e') then
                
            if (direction == "LEFT" and (clonePos - exitPos) > 0) 
            or (direction == "RIGHT" and (clonePos - exitPos) < 0) then
                if not done then
                    print("WAIT")
                    io.stderr:write("293\n")
                    done = true
                end
            else
                if not done then
                    print("BLOCK")
                    io.stderr:write("299\n")
                    done = true
                end
            end
        else
            index = -1
            for i = 0,width-1 do
                if cloneFloor >= 0 then
                    if level[cloneFloor][i] == 'l' then
                        index = i
                        break
                    end
                end
            end
            if index == clonePos then
                if not done then
                    print("WAIT")
                    io.stderr:write("314\n")
                    done = true
                end
            elseif index > clonePos then
                if direction == "RIGHT" then
                    if not done then
                        print("WAIT")  
                        io.stderr:write("321\n")
                        done = true
                    end
                elseif direction == "LEFT" then
                    if cloneFloor > 0 then
                        if level[cloneFloor -1][clonePos] == 'l' then
                            if not done then
                                print("WAIT")
                                io.stderr:write("329\n")
                                done = true
                            end
                        else
                            if not done then
                                print("BLOCK")
                                io.stderr:write("335\n")
                                done = true
                            end
                        end
                    else
                        if not done then
                            print("BLOCK")
                            io.stderr:write("342\n")
                            done = true
                        end
                    end
                else
                    if direction == "LEFT" then
                        if not done then
                            print("WAIT")
                            io.stderr:write("350\n")
                            done = true
                        end
                    elseif direction == "RIGHT" then
                        if cloneFloor > 0 then
                            if level[cloneFloor -1][clonePos] == 'l' then
                                if not done then
                                    print("WAIT")
                                    io.stderr:write("358\n")
                                    done = true
                                end
                            else
                                if not done then
                                    print("BLOCK")
                                    io.stderr:write("364\n")
                                    done = true
                                end
                            end
                        else
                            if not done then
                                print("BLOCK")
                                io.stderr:write("371\n")
                                done = true
                            end
                        end
                    end
                end
            else
                io.stderr:write("<\n")
                if direction == "LEFT" then
                    if not done then
                        print("WAIT")  
                        io.stderr:write("227\n")
                        done = true
                    end
                elseif direction == "RIGHT" then
                    if cloneFloor > 0 then
                        if level[cloneFloor -1][clonePos] == 'l' then
                            if not done then
                                print("WAIT")
                                io.stderr:write("235\n")
                                done = true
                            end
                        else
                            if not done then
                                print("BLOCK")
                                io.stderr:write("241\n")
                                done = true
                            end
                        end
                    else
                        if not done then
                            print("BLOCK")
                            io.stderr:write("248\n")
                            done = true
                        end
                    end
                else
                    if direction == "RIGHT" then
                        if not done then
                            print("WAIT")
                            io.stderr:write("256\n")
                            done = true
                        end
                    elseif direction == "LEFT" then
                        if cloneFloor > 0 then
                            if level[cloneFloor -1][clonePos] == 'l' then
                                if not done then
                                    print("WAIT")
                                    io.stderr:write("264\n")
                                    done = true
                                end
                            else
                                if not done then
                                    print("BLOCK")
                                    io.stderr:write("270\n")
                                    done = true
                                end
                            end
                        else
                            if not done then
                                print("BLOCK")
                                io.stderr:write("217\n")
                                done = true
                            end
                        end
                    end
                end
            end
        end
    end
    if not done then 
        print("WAIT")
        io.stderr:write("382\n")
    end
end