const { request } = require('express');
var mqtt = require('mqtt');
const express=require('express');
const app = express();

app.use(express.json());

var client  = mqtt.connect("mqtt://test.mosquitto.org",{clientId:"mqttjs01"});
console.log("connected flag  "+client.connected);
client.on("connect",function(){	
console.log("connected  "+client.connected);
client.end();
})  

const courses = [
  {id:1, name:'course1'},
  {id:2, name:'course2'},
  {id:3, name:'course3'},
];

app.get('/',(req,res)=>
{
  res.send('Sample Project');
});

app.get('/api/courses',(req,res)=>
{
  res.send('[1,2,3]');
});

app.get('/api/courses/:id',(req,res)=>
{
  const course = courses.find(c => c.id === parseInt(req.params.id));  
  if(!course)   res.status(404).send('Id not found');
  res.send(course);  
});

app.post('/api/courses',(req,res)=>
{
  const course = {
      id: courses.length + 1,
      name: req.body.name
  };
courses.push(course);
res.send(course);
});

const port = process.env.PORT || 3000;
app.listen(port,()=>console.log('Listening on...'))