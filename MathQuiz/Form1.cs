using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MathQuiz
{
    public partial class Form1 : Form
    {
        // Create a Random object called randomizer
        // to generate random numbers.
        Random randomizer = new Random();

        int addend1;
        int addend2;

        int timeLeft;

        /// <summary>
        /// Start the quiz by filling in all of the problems
        /// and starting the timer.
        /// </summary>
        public void StartTheQuiz()
        {
            // Fill in the problems
            SetupProblem(plusLeftLabel, plusRightLabel, sum);
            SetupProblem(minusLeftLabel, minusRightLabel, difference);
            SetupProblem(timesLeftLabel, timesRightLabel, product);
            SetupProblem(dividedLeftLabel, dividedRightLabel, quotient);

            // Start the timer
            timeLeft = 30;
            timeLabel.Text = "30 seconds";
            timer1.Start();
        }

        private void SetupProblem(Label left, Label right, NumericUpDown answer)
        {
            int leftNum, rightNum;

            // Fill in the addition problem.
            // Generate two random numbers to add.
            // Store the values in the variables 'addend1' and 'addend2'
            if (left.Name == "plusLeftLabel")
            {
                leftNum = addend1 = randomizer.Next(51);
                rightNum = addend2 = randomizer.Next(51);
            }
            else
            {
                leftNum = randomizer.Next(51);
                rightNum = randomizer.Next(51);
            }

            // Convert the two randomly generated numbers
            // into strings so that they can be displayed
            // in the label controls.
            left.Text = leftNum.ToString();
            right.Text = rightNum.ToString();

            // 'sum' is the name of the NumericUpDown control.
            // THis step makes sure its value is zero before
            // adding any values to it. 
            answer.Value = 0;
        }

        /// <summary>
        /// Check the answer to see if the user got everything right. 
        /// </summary>
        /// <returns>True if the answer's correct, false otherwise.</returns>
        private bool CheckTheAnswer()
        {
            if (addend1 + addend2 == sum.Value)
                return true;
            else
                return false;
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            StartTheQuiz();
            startButton.Enabled = false;
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (CheckTheAnswer())
            {
                // If CheckTheAnswer() returns true, then the user 
                // got the answer right. Stop the timer  
                // and show a MessageBox.
                timer1.Stop();
                MessageBox.Show("You got all the answers right!",
                                "Congratulations!");
                startButton.Enabled = true;
            }
            else if (timeLeft > 0)
            {
                // If CheckTheAnswer() return false, keep counting
                // down. Decrease the time left by one second and 
                // display the new time left by updating the 
                // Time Left label.
                timeLeft--;
                timeLabel.Text = timeLeft + " seconds";
            }
            else
            {
                // If the user ran out of time, stop the timer, show 
                // a MessageBox, and fill in the answers.
                timer1.Stop();
                timeLabel.Text = "Time's up!";
                MessageBox.Show("You didn't finish in time.", "Sorry!");
                sum.Value = addend1 + addend2;
                startButton.Enabled = true;
            }
        }

        private void answer_Enter(object sender, EventArgs e)
        {
            // Select the whole answer in the NumericUpDown control.
            NumericUpDown answerBox = sender as NumericUpDown;

            if (answerBox != null)
            {
                int lengthOfAnswer = answerBox.Value.ToString().Length;
                answerBox.Select(0, lengthOfAnswer);
            }
        }
    }
}
