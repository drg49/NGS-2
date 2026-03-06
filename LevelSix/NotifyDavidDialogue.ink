-> start

=== start ===
Well, that was quick. Did you find help?
+ [No, but I found an empty cabin nearby.]
-> resOne("An empty cabin? What good will that do us? We need a telephone. I don't think he's going to make it if we wait much longer. He's not talking anymore. I think he's having a stroke!")

=== resOne(response) ===
{response}
+ [It's very close by, and there's food and water there.]
-> resTwo("Are you suggesting we break into someone's home? People will blow our heads off out here. There's absolutely nobody around to help us. It's dangerous, illegal, and stupid to drag him into someone's house.")
+ [It's better than him lying on the dirt. The cabin is close enough for us to carry him there.]
-> resTwo("Are you suggesting we break into someone's home? People will blow our heads off out here. There's absolutely nobody around to help us. It's dangerous, illegal, and stupid to drag him into someone's house.")

=== resTwo(response) ===
{response}
+ [I seriously don't think anyone lives there.]
-> resThree("I don't care! You were supposed to find help. Instead you're trying to get us killed! Also, Marcus told me about how you were acting last night. Have you lost your mind or something? I knew I couldn't count on you. If we wait any longer, he's going to die out here!")
+ [It looks like a vacation home. There aren't any cars in the driveway.]
-> resThree("I don't care! You were supposed to find help. Instead you're trying to get us killed! Also, Marcus told me about how you were acting last night. Have you lost your mind or something? I knew I couldn't count on you. If we wait any longer, he's going to die out here!")

=== resThree(response) ===
{response}
+ [You need to trust me on this one.]
-> lastRes("Okay, fine. We'll carry him there. But only because it's getting dark and we don't have enough time to get back to camp. We'll spend the night there, and early tomorrow morning we run back to camp and get the car.")
+ [If someone does live there, maybe they can help us.]
-> lastRes("Okay, fine. We'll carry him there. But only because it's getting dark and we don't have enough time to get back to camp. We'll spend the night there, and early tomorrow morning we run back to camp and get the car.")

=== lastRes(response) ===
{response}

-> END
