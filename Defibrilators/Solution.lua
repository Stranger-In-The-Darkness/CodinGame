-- Auto-generated code below aims at helping you parse
-- the standard input according to the problem statement.

local function split(str,pat)
  local tbl={}
  i = 0
  
  s = str
  
  while (string.len(s) > 0) do
      ind = s:find(pat)
      if (ind == nil) then 
        tbl[i] = s:sub(1, string.len(s))
        break 
      end
      tbl[i] = s:sub(1, ind-1)
      s = s:sub(ind + 1, string.len(s))
      i = i+1
  end
  
  return tbl
end


LON = io.read()
LAT = io.read()
N = tonumber(io.read())

lon = tonumber(LON:gsub("%,", "."):sub(1, -2)) * math.pi / 180
lat = tonumber(LAT:gsub("%,", "."):sub(1, -2)) * math.pi / 180

name = ""
minDist = -1

for i=0,N-1 do
    DEFIB = io.read()
    
    c = 0
    
    n = ""
    defLon = 0
    defLat = 0
    
    spl = split(DEFIB, ";")

    
    n = spl[1]
    defLon = tonumber(spl[4]:gsub("%,", "."):sub(1, -2)) * math.pi / 180;
    defLat = tonumber(spl[5]:gsub("%,", "."):sub(1, -2)) * math.pi / 180;
    
    d = math.sqrt(
        math.pow(
            (defLon - lon) * math.cos((lat+defLat)/2), 
            2) 
        + math.pow(defLat - lat, 2)) * 6371;
    
    if (minDist == -1) then
        minDist = d
        name = n
    elseif (d < minDist) then
        minDist = d
        name = n
    end
    
    
end

-- Write an action using print()
-- To debug: io.stderr:write("Debug message\n")

print(name)