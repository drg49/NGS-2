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
    + [Continue]
        -> resThree("[Marcus]: Guys please help me. I'm not joking. Call 911...")

=== resThree(response) ===
{response}
    + [Alright. David help me pick him up, we need to find a place with reception. ]
        -> resFour("[David]: Do you realize how far out we are? It would be smarter for me to stay with him while you run down the road and find help...")

=== resFour(response) ===
{response}
    + [Okay. You guys stay here, I'm going to go find us some help.]
        -> lastRes("[David]: We will stay here. But hurry up because it is getting dark soon. If you can't find anyone before 5 PM, run back here and we will carry him out. I have my flashlight with me.")
    + [No way man! We stick together.]
        -> resFive("[David]: Are you stupid? We're not going to drag him down the mountain. Go find the park rangers and tell them which trail we are on. I have my flashlight.")

=== resFive(response) ===
{response}
    + [Alright fine. But stay with Marcus and don't go anywhere. I will be back.]
        -> lastRes("[David]: We will stay here. But hurry up because it is getting dark soon. If you can't find anyone before 5 PM, run back here and we will carry him out. I have my flashlight with me.")

=== lastRes(response) ===
{response}       

-> END
