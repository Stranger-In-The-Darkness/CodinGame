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