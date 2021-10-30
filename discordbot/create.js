const Discord = require("discord.js")


const intents = new Discord.Intents(32767)
const { readdirSync } = require('fs')

const client = new Discord.Client({ intents })
client.on('messageCreate', async (message) => {

    let prefix = '&'

    if(message.channel.type === 'dm') return;
    if(message.author.bot) return;
    if(!message.content.startsWith(prefix)) return;

    const args = message.content.slice(prefix.length).trim().split(/ +/g)
    const command = args.shift().toLowerCase()

    const cmd = readdirSync('./commands').find(file => file.startsWith(command))
    if(cmd) await require(`./commands/${cmd}`)(message, args)
})

client.login("OTAzNzExOTYyNjI0NTg1NzY4.YXw9Wg._CASJfPexF8S9tttcN_N-zLTqfc")
