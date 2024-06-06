using Raylib_cs;
using System.Numerics;

public class Program
{
    static string title = "Game Title"; // Window title
    static int screenWidth = 1000; // Screen width
    static int screenHeight = 800; // Screen height
    static int targetFps = 60; // Target frames-per-second

    static Rectangle player; // Player's rectangle
    static Vector2 velocity; // Player's velocity
    static float acceleration = 0.5f; // Acceleration rate
    static float maxSpeed = 5.0f; // Maximum speed
    static float friction = 0.1f; // Friction rate

    static void Main()
    {
        // Create a window to draw to. The arguments define width and height
        Raylib.InitWindow(screenWidth, screenHeight, title);
        // Set the target frames-per-second (FPS)
        Raylib.SetTargetFPS(targetFps);
        // Setup your game. This is a function YOU define.
        Setup();
        // Loop so long as window should not close
        while (!Raylib.WindowShouldClose())
        {
            // Enable drawing to the canvas (window)
            Raylib.BeginDrawing();
            // Clear the canvas with one color
            Raylib.ClearBackground(Raylib_cs.Color.RayWhite);
            // Your game code here. This is a function YOU define.
            Update();
            // Stop drawing to the canvas, begin displaying the frame
            Raylib.EndDrawing();
        }
        // Close the window
        Raylib.CloseWindow();
    }

    static void Setup()
    {
        // Initialize the player's rectangle
        player = new Rectangle(screenWidth / 2 - 25, screenHeight / 2 - 25, 50, 50);
        velocity = new Vector2(0, 0);
    }

    static void Update()
    {
        // Accelerate the player's rectangle based on input
        if (Raylib.IsKeyDown(KeyboardKey.W)) velocity.Y -= acceleration;
        if (Raylib.IsKeyDown(KeyboardKey.S)) velocity.Y += acceleration;
        if (Raylib.IsKeyDown(KeyboardKey.A)) velocity.X -= acceleration;
        if (Raylib.IsKeyDown(KeyboardKey.D)) velocity.X += acceleration;

        // Clamp the velocity to the maximum speed
        if (velocity.Length() > maxSpeed)
        {
            velocity = Vector2.Normalize(velocity) * maxSpeed;
        }

        // Apply friction to slow down the player when no keys are pressed
        if (!Raylib.IsKeyDown(KeyboardKey.W) && !Raylib.IsKeyDown(KeyboardKey.S))
        {
            if (velocity.Y > 0) velocity.Y -= friction;
            else if (velocity.Y < 0) velocity.Y += friction;
        }

        if (!Raylib.IsKeyDown(KeyboardKey.A) && !Raylib.IsKeyDown(KeyboardKey.D))
        {
            if (velocity.X > 0) velocity.X -= friction;
            else if (velocity.X < 0) velocity.X += friction;
        }

        // Stop the player if the velocity is very low
        if (velocity.Length() < friction)
        {
            velocity = Vector2.Zero;
        }

        // Update the player's position based on velocity
        player.X += velocity.X;
        player.Y += velocity.Y;

        // Draw the player's rectangle
        Raylib.DrawRectangleRec(player, Raylib_cs.Color.Blue);
    }
}
