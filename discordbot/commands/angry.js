
const axios = require('axios')
const Discord = require("discord.js")
const { MessageEmbed } = require('discord.js');

module.exports = async (message, args) => {
  const author = message.mentions.length
    ? message.mentions[0]
    : message.author;
  const { data } = await axios("https://anime-reactions.uzairashraf.dev/api/reactions/random?category=angry").catch(err => {})
  if(!data) return message.channel.send({
    "channel_id": message.channel_id,
    "content": "Algo esta mal",
  });

  console.log(data.reaction)

  const exampleEmbed = new MessageEmbed()
	.setColor('#0099ff')
	//.setTitle('Some title')
	//.setURL('https://discord.js.org/')
	//.setAuthor('Some name', 'https://i.imgur.com/AfFp7pu.png', 'https://discord.js.org')
	//.setDescription('Some description here')
	//.setThumbnail('https://i.imgur.com/AfFp7pu.png')
	//.addFields(
	//	{ name: 'Regular field title', value: 'Some value here' },
	//	{ name: '\u200B', value: '\u200B' },
	//	{ name: 'Inline field title', value: 'Some value here', inline: true },
	//	{ name: 'Inline field title', value: 'Some value here', inline: true },
	//)
	//.addField('Inline field title', 'Some value here', true)
	.setImage(data.reaction)
	//.setFooter('Some footer text here', 'https://i.imgur.com/AfFp7pu.png');
  //  https://discordjs.guide/popular-topics/embeds.html#using-the-embed-constructor

  await message.channel.send({
    "channel_id": message.channel_id,
    content:
      author.id === message.author.id
        ? `**<@${author.id}> esta enojado**`
        : `**<@${message.author.id}> esta enojado con <@${author.id}>**`,
        embeds: [exampleEmbed]
    });
  }