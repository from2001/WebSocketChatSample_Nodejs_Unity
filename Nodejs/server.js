// server.js 
const WebSocket = require('ws');

const wss = new WebSocket.Server({ port: 8080 });

wss.on('connection', function connection(ws) {
    ws.on('message', function incoming(message) {
        console.log('received: %s', message);

        // メッセージを受け取ったら、接続されている全クライアントにそれをブロードキャストする
        wss.clients.forEach(function each(client) {
            if (client.readyState === WebSocket.OPEN) {
                client.send(message);
            }
        });
    });

    ws.send('サーバーからのメッセージ: 接続されました');
});

console.log('WebSocketサーバーがポート8080で起動しました');
