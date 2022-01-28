BounceBehaviour = {}
xlua.private_accessible(CS.GameManager)

local keycode = CS.UnityEngine.KeyCode
local input = CS.UnityEngine.Input
local ball = CS.GameManager.ball
local platformPrefab = CS.GameManager.platformPrefab
local jumpForce = 100
local moveSpeed = 10
local hit

BounceBehaviour.init = function ()
    -- init Lua framework modules
    print("init")
    -- end
    -- ball = CS.UnityEngine.GameObject.Instantiate(CS.GameManager.ballPrefab, CS.GameManager.ballPos.position, CS.UnityEngine.Quaternion.identity);
    -- rb = ball.transform:GetComponent("Rigidbody");

end

BounceBehaviour.update = function ()
    -- print("come to update!")
    -- if (ball ~= nil )
    -- then
        
        if (CS.UnityEngine.Input.GetMouseButtonDown(0))
        then
            print("come here!")
            local ray = CS.UnityEngine.Camera.main:ScreenPointToRay(input.mousePosition);
            -- CS.UnityEngine.RaycastHit hit;
            local flag,hit = CS.GameManager.RayFunction(ray)
            if (CS.UnityEngine.Physics.Raycast(ray, hit))
            then
                print(hit.point);
                local cube = CS.UnityEngine.GameObject.Instantiate(platformPrefab, hit.point + CS.UnityEngine.Vector3(0, 1, 0), CS.UnityEngine.GameObject.transform.rotation);
            end
        end
    
        if (CS.UnityEngine.Input.GetKey(keycode.LeftArrow))
        then
            print("left" + ball.transform.position)
            ball.transform.position = ball.transform.position + moveSpeed * CS.UnityEngine.Vector3.left * CS.UnityEngine.Time.deltaTime;
        end
    
        if (CS.UnityEngine.Input.GetKey(keycode.RightArrow))
        then
            print("right" + ball.transform.position)
            ball.transform.position = ball.transform.position + moveSpeed * CS.UnityEngine.Vector3.right * CS.UnityEngine.Time.deltaTime;
        end
    -- end

end

BounceBehaviour.fixedUpdate = function ()
    -- if (ball ~= nil) 
    -- then
        -- print(ball)
        if (CS.UnityEngine.Input.GetKey(keycode.Space))
        then
            print("Jump jump jump!!!");
            CS.GameManager.rb:AddForce(CS.UnityEngine.Vector3.up * jumpForce);
        end
    -- end
end

