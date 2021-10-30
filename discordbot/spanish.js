
const axios = require('axios')
const Discord = require('discord.js')
const intents = new Discord.Intents(32767)
const client = new Discord.Client({ intents })
const translate = require('translate')
translate.engine = "google"; // Or "yandex", "libre", "deepl"

const bot_name = "Seon-Yil" //Your bot name
const channel_id = "903727183720235018"

client.on('messageCreate', async (message) => 
{
    
    if(message.channel.id !== channel_id) return;
    const messagex = await translate(message.channel.content, "en")
    console.log("verified");
    let data = await axios(`https://api.affiliateplus.xyz/api/chatbot?message=${encodeURIComponent(messagex)}&botname=${encodeURIComponent(bot_name)}&ownername=Arn-Studios&birthplace=Korea`)
    //const translated = await translate(data.message, "es");
    const translated = data.message
    console.log(data);
    console.log(translated);

    message.channel.send({
    channel_id: message.channel.id,
    content: translated,
    message_reference: {
        message_id: message.channel.id}})})


client.login("OTAzNzExOTYyNjI0NTg1NzY4.YXw9Wg._CASJfPexF8S9tttcN_N-zLTqfc")