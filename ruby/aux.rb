def pformat(evt,hash)
  puts "#{evt} -> "+hash.keys.map{|k| "#{k}:#{hash[k]}"}.join(",")
end

def convert_hash(hash)
  retval = System::Collections::Hashtable.new()
  hash.each do |k,v|
    retval.Add( k.to_s.to_clr_string, v.to_s.to_clr_string )
  end
  retval
end

def convert_array(ary)
  System::Array[System::String].new( ary.map { |s| s.to_s.to_clr_string } )
end

def convert_load(hash)
  tmp = System::Collections::Hashtable.new
  hash.each do |k,v|
    if v.respond_to? :to_ary then
      tmp.Add(k.to_s.to_clr_string,convert_array(v))
    else
      tmp.Add(k.to_s.to_clr_string,convert_hash(v))
    end
  end
  tmp
end
