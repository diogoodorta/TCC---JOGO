package com.example.ecomanga2;

import androidx.appcompat.app.AppCompatActivity;
import androidx.core.splashscreen.SplashScreen;

import android.content.Context;
import android.content.Intent;
import android.os.Handler;
import android.os.Vibrator;
import android.view.View;
import android.os.Bundle;
import android.view.ViewTreeObserver;
import android.widget.ImageButton;


public class MainActivity extends AppCompatActivity {
    boolean isReady = false;
    ImageButton BtnCredito ;
    ImageButton BtnFases ;


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        SplashScreen splashScreen = SplashScreen.installSplashScreen(this);
        View content = findViewById(android. R.id.content);
        content.getViewTreeObserver().addOnPreDrawListener(new ViewTreeObserver.OnPreDrawListener() {
            @Override
            public boolean onPreDraw() {
                if(isReady){
                    content.getViewTreeObserver().removeOnPreDrawListener(this);
                }

                    dismissSplashScreen();

                return false;
            }
        });
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);


         BtnCredito = (ImageButton) findViewById(R.id.BtnCredito);
         BtnFases = (ImageButton) findViewById(R.id.BtnFases);
    }

    private void dismissSplashScreen() {
        new Handler().postDelayed(new Runnable() {
            @Override
            public void run() {

                isReady = true;

            }
        },2000);
    }

    private void vibrar() {

        Vibrator rr = (Vibrator) getSystemService(Context.VIBRATOR_SERVICE);
        long milisegundos = 100;
        rr.vibrate(milisegundos);

    }

    public void IrParaTelaCredito (View view){
        Intent novatela = new Intent ( MainActivity.this, credito.class);
        startActivity(novatela);

        vibrar();
    }

    public void IrParaTelaFases (View view){
        Intent novatela = new Intent ( MainActivity.this, fases.class);
        startActivity(novatela);

        vibrar();
    }


    }
