-> start

=== start ===
Wow, I can’t believe we’ve already made it this far. We still have a long drive ahead before we reach the campground.
    + [Yep, we're pretty far away from home.]
        -> resOne("Yeah. About 4 hours out.")
    + [How much longer to go?]
        -> resTwo("We have about 2 more hours until we make it into the mountains. There, we should start seeing more trees.")

=== resOne(response) ===
{response}
    + [Anything you need from inside?]
        -> lastRes("Yeah, we'll need some food and drinks. If there's anything else you think we will need, grab it.")
    + [What time will we get to the campground?]
        -> lastRes("Later in the afternoon, probably around 7 PM before it gets dark. Could you grab some food and supplies inside the gas station?")

=== resTwo(response) ===
{response}
    + [Looking forward to getting out of this barren wasteland.]
        -> lastRes("Me too. Can you go inside and get us some food and drinks for the campsite? We don't have much in the cooler we brought. Grab other supplies too.")
    + [Anything you need from inside?]
        -> lastRes("Yeah, could you get us some food and drinks? We will need some for tonight. Grab anything else that we'll need.")

=== lastRes(response) ===
{response}

-> END
