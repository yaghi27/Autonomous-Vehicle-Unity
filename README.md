# Autonomous Vehicle AI Model (Unity)

## Overview
This project implements an **Autonomous Vehicle AI Model** in Unity, leveraging **ML-Agents** for reinforcement learning. The vehicle navigates an urban environment, adhering to road rules while avoiding obstacles.

## Features
- **Navigation**: The vehicle follows a predefined side of the road.
- **Obstacle Avoidance**: Dynamically adjusts steering to avoid obstacles.
- **ML-Agents Integration**: Uses Unity's ML-Agents for training and inference.

## Requirements
- Unity 2022+
- ML-Agents Toolkit
- Python 3.8+
- TensorFlow
- NVIDIA GPU (optional for faster training)

## Training the Model

To train the autonomous vehicle AI:

1. Open a terminal in the Unity project directory.

2. Create a Python virtual environment:

3. python -m venv venv

4. source venv/bin/activate  # (or venv\Scripts\activate on Windows)

5. Run the ML-Agents training command:

6. mlagents-learn config/training_config.yaml --run-id=autonomous_vehicle

We recommend training with ML-Agents' default configuration as follows:

Start with two mini cities, one consisting of only left turns and the other of only right turns.

Train for 750k-1M steps, setting up checkpoints on both the correct and incorrect sides of the road.

Move to a more complex city with both left and right turns for another 750k-1M steps.

Gradually introduce obstacles bit by bit to refine the model’s decision-making.

### Ray Perception Setup

Add Ray Perception 3D to the vehicle in Unity.

Configure the following parameters:

Max Ray Degrees: 90°

Ray Distance: 17

Rays Per Side: 6-9

## Usage
- After training, load the trained model into the Unity scene.
- Run the simulation to observe how the vehicle reacts to different road conditions.

## Future Enhancements
- Improve traffic light detection and response.
- Add pedestrian and cyclist detection.
- Optimize training for real-time decision-making.

## License
This project is open-source under the MIT License.

