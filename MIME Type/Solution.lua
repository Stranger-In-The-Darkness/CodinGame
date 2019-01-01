-- Auto-generated code below aims at helping you parse
-- the standard input according to the problem statement.

local function split(str,pat)
  local tbl={}
  i = 1
  
  s = str
  
  while (string.len(s) > 0) do
      ind = s:find(pat, 1, true)
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

N = tonumber(io.read()) -- Number of elements which make up the association table.
Q = tonumber(io.read()) -- Number Q of file names to be analyzed.

hashtable = {}

for i=0,N-1 do
    -- EXT: file extension
    -- MT: MIME type.
    next_token = string.gmatch(io.read(), "[^%s]+")
    EXT = next_token()
    MT = next_token()
    
    hashtable[EXT:lower()] = MT
end
for i=0,Q-1 do
    FNAME = io.read() -- One file name per line.
    
    splitStr = split(FNAME, ".")
    
    if hashtable[splitStr[#splitStr]:lower()] ~= nil and #splitStr > 1 and FNAME:sub(#FNAME, -1) ~= "." then
        print(hashtable[splitStr[#splitStr]:lower()])
    else
        print("UNKNOWN")
    end

end

-- Write an action using print()
-- To debug: io.stderr:write("Debug message\n")


-- For each of the Q filenames, display on a line the corresponding MIME type. If there is no corresponding type, then display UNKNOWN.