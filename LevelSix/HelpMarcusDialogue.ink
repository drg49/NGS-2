-> start

=== start ===
[Marcus]: Aaaagh!
    + [Yo Marcus! Are you okay man?!]
        -> resOne("[Marcus]: No! Help me!")
    + [Marcus what happened? Are you alright?]
        -> resOne("[Marcus]: No! Help me!")

=== resOne(response) ===
{response}
    + [What is wrong with you?]
        -> resTwo("[Marcus]: I can't move my body, something is wrong with me. I can't feel my body!")

=== resTwo(response) ===
{response}
    + [What do you mean "you can't move your body"? What the hell is happening?!]
        -> resThree("[Marcus]: I don't know! I have no idea what's going on with me. Everything hurts and I can't feel my body. You guys need to help me. This is not a joke. Seriously, call 911... Please!")

=== resThree(response) ===
{response}
    + [Alright. David help me pick him up, we need to find a place with reception. ]
        -> resFour("[David]: Do you realize how far out we are? It would be smarter for me to stay here with him while you run down the road and find help...")

=== resFour(response) ===
{response}
    + [Okay. You guys stay here, I'm going to go find us some help.]
        -> lastRes("[David]: We will stay here. But hurry up because it is getting dark soon. If you can't find anyone before 6 PM, we will have to spend the night out here. I don't have my flashlight, but I have my lighter with me and I'll start a fire if needed. These woods become pitch black at night and we will get lost easily if we head back too late.")
    + [No way man! We stick together.]
        -> resFive("[David]: Are you stupid? We're not going to drag him down the mountain. Go find the park rangers and tell them which trail we are on.")

=== resFive(response) ===
{response}
    + [Alright fine. But stay with Marcus and don't go anywhere. I will be back.]
        -> lastRes("[David]: We will stay here. But hurry up because it is getting dark soon. If you can't find anyone before 6 PM, we will have to spend the night out here. I don't have my flashlight, but I have my lighter with me and I'll start a fire if needed. These woods become pitch black at night and we will get lost easily if we head back too late.")

=== lastRes(response) ===
{response}       

-> END
