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
		queue.count = 0
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