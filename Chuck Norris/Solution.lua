-- Auto-generated code below aims at helping you parse
-- the standard input according to the problem statement.

function Crypt (s)
    res = "";
    if s:find("0") ~= nil then
        res = res .. "00 "
    else
        res = res .. "0 "
    end
    for i = 1, s:len() do
        res = res .. '0'
    end
    return res
end

function getBytes(b)
    res = ""
    for i = 1,7 do
        res = tostring(b % 2):sub(1,1) .. res;
        b = (b - b % 2) / 2;
    end
    return res
end

function getSameSymbolRow(s)
    last = ' '
    for i = 1, s:len() do
        if last == " " then
            last = s:sub(i,i)
        else
            if s:sub(i,i) == last:sub(-1) then
                last = last .. s:sub(i,i)
            else
                break
            end
        end
    end
    return last
end

MESSAGE = io.read()

io.stderr:write(MESSAGE..'\n')

encoded = ""
bitCode = ""

for i = 1,#MESSAGE do
    b = string.byte(MESSAGE)
    b = getBytes(b)
    bitCode = bitCode .. b
end

split = {}
ose = false

if bitCode:sub(-2, -2) ~= bitCode:sub(-1) then
    ose = true
end

io.stderr:write("bitcode = "..bitCode .. "\n")

repeat
    index = 1
    split[#split+1] = getSameSymbolRow(bitCode);
    index = index + split[#split]:len()
    bitCode = bitCode:sub(index)
until bitCode:len() <= 1

if ose then
    split[#split+1] = bitCode
end

for index,s in ipairs(split) do
    encoded = encoded .. Crypt(s).." "
end

encoded = encoded:sub(0, encoded:len()-1)
io.stderr:write(encoded .. "\n\nprinted =  ")

print(encoded)