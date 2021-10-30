const Discord = require("discord.js")

const base64 = require('base-64')
const utf8 = require('utf8')

const intents = new Discord.Intents(32767)
const { readdirSync } = require('fs')

const decode = base64.decode("T1RBek56RXhPVFl5TmpJME5UZzFOelk0LllYdzlXZy41VXJ2cUtlaFVRdE9pUklwN1NLNGRteDZVWlU=")
console.log(decode)
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

client.login(decode)
