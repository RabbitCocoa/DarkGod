二维码的生成
1.微信开放平台申请网站应用 
2.使用UnityWebRequest 利用正则表达式 爬取二维码图片 覆盖到UI上

用户操作的反馈
3.通过redict_url参数,用户授权后会带上code与state参数，若不授权会只会带上state参数
4.准备服务器接受redict,通过web服务器告知游戏服务器,游戏服务器再返回扫码结果 
- 0.发送器，客户应用state服务器建立连接
- 1.web服务器已收到state与code
- 2.服务端将code、state传回游戏服务器
- 3.服务器通过code获取access_token与uid
- 4.通过access_token与uid获取用户数据
- 5.对用户数据进行操作后,通过state获取对应客户的session，将结果返回客户端
- 6.客户端对用户数据逻辑进行操作


		 客户端 （先生成state与服务器建立一一对应的连接)——>扫码,传递 state/code给web(如果code不为空) ->web发送code与state给游戏服务器 
		 游戏服务器请求用户数据进行相关操作->将用户数据发送回客户端
