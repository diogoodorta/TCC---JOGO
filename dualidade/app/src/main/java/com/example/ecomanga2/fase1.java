package com.example.ecomanga2;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.os.Vibrator;
import android.view.View;

public class fase1 extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_fase1);
    }

    private void vibrar() {

        Vibrator rr = (Vibrator) getSystemService(Context.VIBRATOR_SERVICE);
        long milisegundos = 100;
        rr.vibrate(milisegundos);

    }

    public void IrParaTelaInimigo (View view){
        Intent novatela = new Intent ( fase1.this, inimigos.class);
        startActivity(novatela);

        vibrar();
    }

    public void IrParaTelaCenario (View view){
        Intent novatela = new Intent ( fase1.this, cena1.class);
        startActivity(novatela);

        vibrar();
    }

    public void IrParaTelaChefoes (View view){
        Intent novatela = new Intent ( fase1.this, boss1.class);
        startActivity(novatela);

        vibrar();
    }



}