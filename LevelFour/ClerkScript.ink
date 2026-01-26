-> start

=== start ===
Hello sir, how are you?
    + [Doing okay, how about you?]
        -> resOne("Doing great. Find everything you needed?")
    + [Fine.]
        -> resTwo("So... What the hell are you guys doing all the way out here?")

=== resOne(response) ===
{response}
    + [I think so.]
        -> lastRes("Well okay, $45 is your total. Also, try not to be out too late around here. People have gone missing...")
    + [Yes. Do you know if the Whispering Pines campground is far from here?]
        -> lastRes("No you'll get there before dark if you don't make any side trips. You don't want to be out too late. People have gone missing around here...")

=== resTwo(response) ===
{response}
    + [We are on our way to the Whispering Pines campground.]
        -> lastRes("Well be safe. There's been a few people who went missing in this area lately.")
    + [Not much, just checking the area out.]
        -> lastRes("Umm... Okay. $45 is your total. Also, not sure if this is the place to be hanging around. We've had a lot of missing people in the area lately.")

=== lastRes(response) ===
{response}

-> END
