# SharpChain
SharpChain is a simple blockchain implementation for educational purposes, written in C#. It is a simpler, smaller model of the Bitcoin blockchain. I believe it makes it easier to see how the blockchain actually works nicely.

## Prerequisites
.NET Core 2.0 SDK

## Build Instructions
If you have Visual Studio, just open the .sln file and build as usual. 
Alternatively, you can do:
```bash
dotnet build
```
in the folder where the .sln file is.

## API Reference

| Query                       | Description|
| -------------               |-------------|
| _SharpChain/Register_        | Gets the user an ID to use with their requests. |
| _SharpChain/Mine/{**ID**}_        | Mines a block using the [Hashcash](https://en.wikipedia.org/wiki/Hashcash) algorithm. The default difficuly is 4, and the reward is 1 SharpCoin.                                  |
| _SharpChain/GetBalance/{**ID**}_  | Gets the balance of {**ID**}.                         |
| _SharpChain/SendCoin/{**senderID**}/{**recipientID**}/{**amount**}_| Send {**amount**} of SharpCoins to {**recipientID**}. The {**senderID**} should be your ID. |
| _SharpChain/GetChain_         | Gets the whole blockchain as JSON. |

## How to Use

First you need to get and ID, like so:
```bash
localhost:9532/SharpChain/Register
```
This will return an ID, like _723e83c4-8f43-4e96-a1e2-af895d041a46_.
After that, SharpChain works exactly like the Bitcoin blockchain. You need to mine blocks with:
```bash
localhost:9532/SharpChain/Mine/723e83c4-8f43-4e96-a1e2-af895d041a46
```
Blocks are needed to store transactions & send coins to someone. You can send coins with:
```bash
localhost:9532/SharpChain/SendCoin/723e83c4-8f43-4e96-a1e2-af895d041a46/f6b88148-b0e9-439c-b246-21e7a55e0c11/5
```
This will send 5 SharpCoins to _f6b88148-b0e9-439c-b246-21e7a55e0c11_, from _723e83c4-8f43-4e96-a1e2-af895d041a46_. The transaction will be added to the next mined block.

You can see your balance anytime with:
```bash
localhost:9532/SharpChain/GetBalance/723e83c4-8f43-4e96-a1e2-af895d041a46
```

If anytime, you want to see the whole chain, you can do:
```bash
localhost:9532/SharpChain/GetChain
```

## What Is Missing?
* Consensus
* Persistance
* UI
