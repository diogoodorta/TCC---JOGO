package com.example.ecomanga2;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Context;
import android.content.Intent;
import android.os.Bundle;
import android.os.Vibrator;
import android.view.View;

public class fase2 extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_fase2);
    }

    private void vibrar() {

        Vibrator rr = (Vibrator) getSystemService(Context.VIBRATOR_SERVICE);
        long milisegundos = 100;
        rr.vibrate(milisegundos);

    }

    public void IrParaTelaInimigo (View view){
        Intent novatela = new Intent ( fase2.this, inimigos2.class);
        startActivity(novatela);

        vibrar();
    }

    public void IrParaTelaCenario (View view){
        Intent novatela = new Intent ( fase2.this, cena2.class);
        startActivity(novatela);

        vibrar();
    }

    public void IrParaTelaChefoes (View view){
        Intent novatela = new Intent ( fase2.this, boss2.class);
        startActivity(novatela);

        vibrar();
    }



}