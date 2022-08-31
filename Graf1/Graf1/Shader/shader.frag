#version 330

out vec4 outputColor;

in vec4 vertexColor;

//uniform vec4 ourColor;
void main()
{
	outputColor = vec4(0.9, 0.8, 0.01, 1.0);
	//outputColor = vertexColor;
	//outputColor = ourColor;
}