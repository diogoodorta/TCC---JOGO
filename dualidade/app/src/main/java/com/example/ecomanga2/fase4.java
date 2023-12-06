package com.example.ecomanga2;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.os.Vibrator;
import android.view.View;

public class fase4 extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_fase4);
    }

    private void vibrar() {

        Vibrator rr = (Vibrator) getSystemService(Context.VIBRATOR_SERVICE);
        long milisegundos = 100;
        rr.vibrate(milisegundos);

    }

    public void IrParaTelaInimigo (View view){
        Intent novatela = new Intent ( fase4.this, inimigos4.class);
        startActivity(novatela);

        vibrar();
    }

    public void IrParaTelaCenario (View view){
        Intent novatela = new Intent ( fase4.this, cena3.class);
        startActivity(novatela);

        vibrar();
    }

    public void IrParaTelaChefoes (View view){
        Intent novatela = new Intent ( fase4.this, boss3.class);
        startActivity(novatela);

        vibrar();
    }



}