export {simpleClone}


function simpleClone(obj) {
  if(obj == null || typeof(obj) != 'object')
      return obj;    
  var temp = new obj.constructor(); 
  for(var key in obj)
      temp[key] = simpleClone(obj[key]);    
  return temp;
}