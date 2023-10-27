package com.example.ecomanga2;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Context;
import android.content.Intent;
import android.os.Vibrator;
import android.view.View;
import android.os.Bundle;


public class fases extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_fases);
    }

    private void vibrar() {

        Vibrator rr = (Vibrator) getSystemService(Context.VIBRATOR_SERVICE);
        long milisegundos = 100;
        rr.vibrate(milisegundos);
    }

        public void IrParaTelaFase1 (View view){
            Intent novatela = new Intent(fases.this, fase1.class);
            startActivity(novatela);

            vibrar();
        }

    public void IrParaTelaFase2 (View view){
        Intent novatela = new Intent(fases.this, fase2.class);
        startActivity(novatela);

        vibrar();
    }

    public void IrParaTelaFase3 (View view){
        Intent novatela = new Intent(fases.this, fase4.class);
        startActivity(novatela);

        vibrar();
    }


}
