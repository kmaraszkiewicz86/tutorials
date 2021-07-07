package com.example.lerning_1

import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.Button
import android.widget.ImageView

class MainActivity : AppCompatActivity() {

    private val dice : Dice = Dice(6);

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)

        val rollButton: Button = findViewById(R.id.button2);
        rollButton.setOnClickListener{
            var diceRollNumber : Int = getDiceImageSrc();

            val imageView: ImageView = findViewById(R.id.imageView2)

            imageView.contentDescription = diceRollNumber.toString()
            imageView.setImageResource(diceRollNumber);
        };
    }

    private fun getDiceImageSrc() : Int {

        return when (dice.roll()) {
            1 -> R.drawable.dice_1
            2 -> R.drawable.dice_2
            3 -> R.drawable.dice_3
            4 -> R.drawable.dice_4
            5 -> R.drawable.dice_5
            else -> R.drawable.dice_6
        }
    }
}

class Dice(private val numSides: Int) {
    fun roll() : Int {
        return (1..numSides).random();
    }
}