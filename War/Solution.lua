Queue = {}

function Queue.new (o)
	return {values = {}, count = 0}
end

function Queue.enqueue (queue, o)
	if queue.values[#queue.values] ~= nil then
		queue.values[#queue.values + 1] = o
		queue.count = queue.count + 1
	else
		queue.values[#queue.values] = o
		queue.count = queue.count + 1
	end	
end

function Queue.dequeue (queue)
	r = queue.values[0]
	queue.count = queue.count - 1
	
	if queue.count <= 0 then
		queue.values = {}
		return r
	end
	
	nv = {}
	
	for i = 1,#queue.values do
	    nv[i-1] = queue.values[i]
    end
	
    queue.values = nv
	
	return r
end

function Queue.count (queue)
	if queue.count <= 0 then
		return 0
	else
		return queue.count
	end
end

function Queue.contains (queue, o)
	if o ~= nil then
		for i = 0, #queue.values do
			if queue.values[i] == o then
				return true
			end
		end
	else
		return false
	end
end

function Queue.clear(queue)
	queue.values = {}
end

alph = {}
alph['2'] = 0
alph['3'] = 1
alph['4'] = 2
alph['5'] = 3
alph['6'] = 4
alph['7'] = 5
alph['8'] = 6
alph['9'] = 7
alph['10'] = 8
alph['J'] = 9
alph['Q'] = 10
alph['K'] = 11
alph['A'] = 12

n = tonumber(io.read())

deckp1 = Queue.new()
deckp2 = Queue.new()

for i = 0,n-1 do
	Queue.enqueue(deckp1, io.read())
end

m = tonumber(io.read())

for i = 0,m-1 do
	Queue.enqueue(deckp2, io.read())
end

io.stderr:write(Queue.count(deckp1).." "..Queue.count(deckp2).."\n")

war = false

result = "PAT"
turns = 0

warp1 = {}
warp2 = {}

while Queue.count(deckp1) >= 0 and Queue.count(deckp2) >= 0 do
	
	io.stderr:write("d1 - " .. Queue.count(deckp1) .. "\n")
	io.stderr:write("d2 - " .. Queue.count(deckp2) .. "\n")
	
	if war then
		io.stderr:write("WAR\n")
	else
		io.stderr:write("TURN " .. turns .. "\n")
	end
	
	if (Queue.count(deckp1) == 0 and Queue.count(deckp2) == 0) then
		result = "PAT"
		io.stderr:write("PAT\n")
		break
	elseif (not war and Queue.count(deckp1) == 0) then
		result = "2"
		io.stderr:write("2\n")
		break
	elseif (not war and Queue.count(deckp2) == 0) then
		result = "1"
		io.stderr:write("1\n")
		break
	end

	if war then
		warp1[#warp1 + 1] = Queue.dequeue(deckp1)
		warp1[#warp1 + 1] = Queue.dequeue(deckp1)
		warp1[#warp1 + 1] = Queue.dequeue(deckp1)
		if (Queue.count(deckp1) == 0) then 
			result = "PAT"
			goto print
		end

		warp2[#warp2 + 1] = Queue.dequeue(deckp2)
		warp2[#warp2 + 1] = Queue.dequeue(deckp2)
		warp2[#warp2 + 1] = Queue.dequeue(deckp2)
		if (Queue.count(deckp2) == 0) then 
			result = "PAT"
			goto print
		end

		warp1[#warp1 + 1] = Queue.dequeue(deckp1)
		warp2[#warp2 + 1] = Queue.dequeue(deckp2)
		
		c1 = warp1[#warp1]
		p1 = c1:sub(1,c1:len() - 1)
		c2 = warp2[#warp2]
		p2 = c2:sub(1, c2:len() - 1)
		
		io.stderr:write(p1.." ")
		io.stderr:write(p2.."\n")

		if alph[p1] > alph[p2] then
			for i = 1,#warp1 do			
				Queue.enqueue(deckp1, warp1[i])
			end
			warp1 = {}
			for i = 1,#warp2 do			
				Queue.enqueue(deckp1, warp2[i])
			end
			warp2 = {}

			war = false
			turns = turns + 1
			goto cont
		elseif alph[p2] > alph[p1] then
			for i = 1,#warp1 do
				Queue.enqueue(deckp2, warp1[i])
			end
			warp1 = {}
			for i = 1,#warp2 do
				Queue.enqueue(deckp2, warp2[i])
			end
			warp2 = {}

			war = false
			turns = turns + 1
			goto cont
		else
			goto cont
		end
	else
		warp1[#warp1 + 1] = Queue.dequeue(deckp1)
		warp2[#warp2 + 1] = Queue.dequeue(deckp2)
		
		c1 = warp1[#warp1]
		p1 = c1:sub(1,c1:len() - 1)
		c2 = warp2[#warp2]
		p2 = c2:sub(1, c2:len() - 1)
		
		io.stderr:write(p1.." ")
		io.stderr:write(p2.."\n")

		if alph[p1] > alph[p2] then
			for i = 1,#warp1 do			
				Queue.enqueue(deckp1, warp1[i])
			end
			warp1 = {}
			for i=1, #warp2 do			
				Queue.enqueue(deckp1, warp2[i])
			end
			warp2 = {}
			turns = turns + 1
			goto cont
		elseif alph[p2] > alph[p1] then
			for i=1,#warp1 do			
				Queue.enqueue(deckp2, warp1[i])
			end
			warp1 = {}
			for i=1, #warp2 do			
				Queue.enqueue(deckp2, warp2[i])
			end
			warp2 = {}
			turns = turns + 1
			goto cont
		else		
			war = true
			goto cont
		end
	end
	::cont::
end

::print::

if result == "PAT" then
	print("PAT")
else
	print(result.." "..turns)
end